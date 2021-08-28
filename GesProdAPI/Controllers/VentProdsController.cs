using AutoMapper;
using Contracts;
using Entities.DataTransfertObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GesProdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VentProdsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public VentProdsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllVentProds([FromQuery] PaginationParameters paginationParameters)
        {
            var ventProds = await _repository.VentProd.GetAllVentProdsAsync(paginationParameters);

            var metadata = new
            {
                ventProds.TotalCount,
                ventProds.PageSize,
                ventProds.CurrentPage,
                ventProds.TotalPages,
                ventProds.HasNext,
                ventProds.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all ventProds ventProds from database.");

            var ventProdsReadDto = _mapper.Map<IEnumerable<VentProdReadDto>>(ventProds);

            return Ok(ventProdsReadDto);
        }



        [HttpGet("{id}", Name = "VentProdById")]
        public async Task<IActionResult> GetVentProdById(Guid id)
        {
            var ventProd = await _repository.VentProd.GetVentProdByIdAsync(id);

            if (ventProd == null)
            {
                _logger.LogError($"VentProd with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned ventProdWriteDto with id: {id}");

                var ventProdReadDto = _mapper.Map<VentProdReadDto>(ventProd);
                return Ok(ventProdReadDto);
            }
        }




        [HttpPost]
        public async Task<IActionResult> CreateVentProd([FromBody] VentProdCreateDto ventProd)
        {
            if (ventProd == null)
            {
                _logger.LogError("VentProd object sent from ventProd is null.");
                return BadRequest("VentProd object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid ventProdWriteDto object sent from ventProd.");
                return BadRequest("Invalid model object");
            }

            var serviceEntity = await _repository.Service.GetServiceByIdAsync(ventProd.ServiceId);
            
            if (serviceEntity == null)
            {
                _logger.LogError($"Service with Id: {ventProd.ServiceId}, hasn't been found.");
                return NotFound($"Service with Id: {ventProd.ServiceId}");
            }

            var ventProdEntity = _mapper.Map<VentProd>(ventProd);
            ventProdEntity.Id = Guid.NewGuid();
            ventProdEntity.PrixVente = serviceEntity.PrixVente;
            await _repository.VentProd.CreateVentProdAsync(ventProdEntity);

            var venteEntity = await _repository.Vente.GetVenteByIdAsync(ventProd.VenteId.Value);

            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);
            venteEntity.VentProds = null;
            await _repository.Vente.UpdateVenteAsync(venteEntity);

            await _repository.SaveAsync();

            var ventProdReadDto = _mapper.Map<VentProdReadDto>(ventProdEntity);
            return CreatedAtRoute("VentProdById", new { id = ventProdReadDto.Id }, ventProdReadDto);
        }
        


/*
        [HttpPost]
        public async Task<IActionResult> CreateVentProd([FromBody] IEnumerable<VentProdCreateDto> ventProds)
        {
            var services = new List<Service>();
            foreach (var ventProd in ventProds)
            {
                if (ventProd == null)
                {
                    _logger.LogError("VentProd object sent from ventProd is null.");
                    return BadRequest("VentProd object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ventProdWriteDto object sent from ventProd.");
                    return BadRequest("Invalid model object");
                }

                var serviceEntity = await _repository.Service.GetServiceByIdAsync(ventProd.ServiceId);

                if (serviceEntity == null)
                {
                    _logger.LogError($"Service with Id: {ventProd.ServiceId}, hasn't been found.");
                    return NotFound($"Service with Id: {ventProd.ServiceId}");
                }
                else
                {
                    services.Add(serviceEntity);
                }

            }

            var ventProdsEntities = _mapper.Map<IEnumerable<VentProd>>(ventProds);
            ventProdEntity.Id = Guid.NewGuid();
            ventProdEntity.PrixVente = serviceEntity.PrixVente;
            await _repository.VentProd.CreateVentProdAsync(ventProdEntity);

            var venteEntity = await _repository.Vente.GetVenteByIdAsync(ventProd.VenteId.Value);

            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);
            venteEntity.VentProds = null;
            await _repository.Vente.UpdateVenteAsync(venteEntity);

            await _repository.SaveAsync();

            var ventProdReadDto = _mapper.Map<VentProdReadDto>(ventProdEntity);
            return CreatedAtRoute("VentProdById", new { id = ventProdReadDto.Id }, ventProdReadDto);
        }
        */


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVentProd(Guid id, [FromBody] VentProdCreateDto ventProdWriteDto)
        {
            if (ventProdWriteDto == null)
            {
                _logger.LogError("VentProd object sent from ventProd is null.");
                return BadRequest("VentProd object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid ventProdWriteDto object sent from ventProd.");
                return BadRequest("Invalid model object");
            }

            var ventProdEntity = await _repository.VentProd.GetVentProdByIdAsync(id);
            if (ventProdEntity == null)
            {
                _logger.LogError($"VentProd with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(ventProdWriteDto, ventProdEntity);

            var oldVenteId = ventProdEntity.VenteId;
            var newVenteId = ventProdWriteDto.VenteId;

            var newServiceEntity = await _repository.Service.GetServiceByIdAsync(ventProdWriteDto.ServiceId);

            if (newServiceEntity == null)
            {
                _logger.LogError($"Service with Id: {ventProdWriteDto.ServiceId}, hasn't been found.");
                return NotFound($"Service with Id: {ventProdWriteDto.ServiceId}");
            }

            ventProdEntity.PrixVente = newServiceEntity.PrixVente;
            await _repository.VentProd.UpdateVentProdAsync(ventProdEntity);

            var newVenteEntity = await _repository.Vente.GetVenteByIdAsync(newVenteId.Value);

            if (newVenteEntity == null)
            {
                _logger.LogError($"Vente with Id: {ventProdWriteDto.VenteId}, hasn't been found.");
                return NotFound($"Vente with Id: {ventProdWriteDto.VenteId}");
            }

            newVenteEntity.MontantTotal = newVenteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);
            newVenteEntity.VentProds = null;
            await _repository.Vente.UpdateVenteAsync(newVenteEntity);

            if (oldVenteId == newVenteId) await _repository.SaveAsync();

            if (oldVenteId != newVenteId)
            {

                var oldServiceEntity = await _repository.Service.GetServiceByIdAsync(ventProdEntity.ServiceId.Value);

                if (oldServiceEntity == null)
                {
                    _logger.LogError($"Service with Id: {ventProdEntity.ServiceId}, hasn't been found.");
                    return NotFound($"Service with Id: {ventProdEntity.ServiceId}");
                }

                var oldVenteEntity = await _repository.Vente.GetVenteByIdAsync(oldVenteId);

                if (oldVenteEntity == null)
                {
                    _logger.LogError($"Vente with Id: {ventProdWriteDto.VenteId}, hasn't been found.");
                    return NotFound($"Vente with Id: {ventProdWriteDto.VenteId}");
                }

                oldVenteEntity = await _repository.Vente.GetVenteByIdAsync(oldVenteId);

                oldVenteEntity.MontantTotal = oldVenteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);
                oldVenteEntity.VentProds = null;
                await _repository.Vente.UpdateVenteAsync(oldVenteEntity);
                await _repository.SaveAsync();
            }

            var ventProdReadDto = _mapper.Map<VentProdReadDto>(newVenteEntity);
            return Ok(ventProdReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentProd(Guid id)
        {
            var ventProd = await _repository.VentProd.GetVentProdByIdAsync(id);

            if (ventProd == null)
            {
                _logger.LogError($"VentProd with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.VentProd.DeleteVentProdAsync(ventProd);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
