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
using System.Security.Claims;
using System.Threading.Tasks;

namespace GesProdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VentesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public VentesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllVentes([FromQuery] PaginationParameters paginationParameters)
        {
            var ventes = await _repository.Vente.GetAllVentesAsync(paginationParameters);

            var metadata = new
            {
                ventes.TotalCount,
                ventes.PageSize,
                ventes.CurrentPage,
                ventes.TotalPages,
                ventes.HasNext,
                ventes.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all ventesDisponible ventesDisponible from database.");

            var ventesReadDto = _mapper.Map<IEnumerable<VenteReadDto>>(ventes);

            return Ok(ventesReadDto);
        }



        [HttpGet("{id}", Name = "VenteById")]
        public async Task<IActionResult> GetVenteById(Guid id)
        {
            var vente = await _repository.Vente.GetVenteByIdAsync(id);

            if (vente == null)
            {
                _logger.LogError($"Vente with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned venteWriteDto with id: {id}");

                var venteReadDto = _mapper.Map<VenteReadDto>(vente);
                return Ok(venteReadDto);
            }
        }





        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}")]
        public async Task<IActionResult> GetTurnover(DateTime debutPeriode, DateTime finPeriode)
        {
            _logger.LogInfo($"Returned Turnover from {debutPeriode} to {finPeriode}.");
            return Ok(await _repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode));
        }





        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/categorie/{categoryId}")]
        public async Task<IActionResult> GetTurnoverCategory(DateTime debutPeriode, DateTime finPeriode, Guid? categoryId)
        {
            _logger.LogInfo($"Returning Category with id {categoryId}.");
            var category = await _repository.Category.GetCategoryByIdAsync(categoryId.Value);

            if (category == null)
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found.");
                return NotFound($"Category with id: {categoryId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned Turnover from {debutPeriode} to {finPeriode} having categoryId equal to '{categoryId}'");
            return Ok(_repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, category));
        }





        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/service/{serviceId}")]
        public async Task<IActionResult> GetTurnoverService(DateTime debutPeriode, DateTime finPeriode, Guid? serviceId)
        {
            _logger.LogInfo($"Returning Service with id {serviceId}.");
            var service = await _repository.Service.GetServiceByIdAsync(serviceId.Value);

            if (service == null)
            {
                _logger.LogError($"Service with id: {serviceId}, hasn't been found.");
                return NotFound($"Service with id: {serviceId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned Turnover from {debutPeriode} to {finPeriode} having serviceId equal to '{serviceId}'");
            return Ok(_repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, service));
        }




        [HttpGet("Chiffre-affaires/{debutPeriode}/{finPeriode}/utilisateur/{utilisateurId}")]
        public async Task<IActionResult> GetTurnoverUtilisateur(DateTime debutPeriode, DateTime finPeriode, Guid? utilisateurId)
        {
            _logger.LogInfo($"Returning Utilisateur with id {utilisateurId}.");
            var utilisateur = await _repository.Utilisateur.GetUtilisateurByIdAsync(utilisateurId.Value);

            if (utilisateur == null)
            {
                _logger.LogError($"Utilisateur with id: {utilisateurId}, hasn't been found.");
                return NotFound($"Utilisateur with id: {utilisateurId}, hasn't been found.");
            }

            _logger.LogInfo($"Returned Turnover from {debutPeriode} to {finPeriode} having utilisateurId equal to '{utilisateurId}'");
            return Ok(_repository.VentProd.GetTurnoverAsync(debutPeriode, finPeriode, utilisateur.Id));
        }





        [HttpPost]
        public async Task<IActionResult> CreateVente([FromBody] VenteCreateDto vente)
        {
            if (vente == null)
            {
                _logger.LogError("Vente object sent from vente is null.");
                return BadRequest("Vente object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid venteWriteDto object sent from vente.");
                return BadRequest("Invalid model object");
            }

            var venteEntity = _mapper.Map<Vente>(vente);

            venteEntity.Id = Guid.NewGuid();
            venteEntity.NumFact = await _repository.Vente.GetNextNumberAsync();
            venteEntity.DateEnr = DateTime.Now;
            venteEntity.TypeFacture = "FV";
            venteEntity.ModePaiement = "A";
            venteEntity.ClientsId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            venteEntity.IdUserEnr = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            //venteEntity.LibelleFacture = $"Reservation Client : {AppUser.FindFirst("Name").Value} {AppUser.FindFirst("").Value} ";
            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);

            await _repository.Vente.CreateVenteAsync(venteEntity);
            await _repository.SaveAsync();

            var venteReadDto = _mapper.Map<VenteReadDto>(venteEntity);
            return CreatedAtRoute("VenteById", new { id = venteReadDto.Id }, venteReadDto);
        }
        




        [HttpPost("for/{clientId}")]
        public async Task<IActionResult> CreateVente([FromBody] VenteCreateDto vente, Guid clientId)
        {
            if (vente == null)
            {
                _logger.LogError("Vente object sent from vente is null.");
                return BadRequest("Vente object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid venteWriteDto object sent from vente.");
                return BadRequest("Invalid model object");
            }

            var venteEntity = _mapper.Map<Vente>(vente);

            venteEntity.Id = Guid.NewGuid();
            venteEntity.NumFact = await _repository.Vente.GetNextNumberAsync();
            venteEntity.DateEnr = DateTime.Now;
            venteEntity.TypeFacture = "FV";
            venteEntity.ModePaiement = "A";
            venteEntity.ClientsId = clientId;
            venteEntity.IdUserEnr = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            //venteEntity.LibelleFacture = $"Reservation Client : {AppUser.FindFirst("Name").Value} {AppUser.FindFirst("").Value} ";
            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);

            await _repository.Vente.CreateVenteAsync(venteEntity);
            await _repository.SaveAsync();

            var venteReadDto = _mapper.Map<VenteReadDto>(venteEntity);
            return CreatedAtRoute("VenteById", new { id = venteReadDto.Id }, venteReadDto);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVente(Guid id, [FromBody] VenteUpdateDto venteWriteDto)
        {
            if (venteWriteDto == null)
            {
                _logger.LogError("Vente object sent from vente is null.");
                return BadRequest("Vente object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid venteWriteDto object sent from vente.");
                return BadRequest("Invalid model object");
            }

            var venteEntity = await _repository.Vente.GetVenteByIdAsync(id);
            if (venteEntity == null)
            {
                _logger.LogError($"Vente with id: {id}, hasn't been found.");
                return NotFound($"Vente with id: {id}, hasn't been found.");
            }

            _mapper.Map(venteWriteDto, venteEntity);


            
            venteEntity.DateEnr = DateTime.Now;
            venteEntity.TypeFacture = "FV";
            venteEntity.ModePaiement = "A";
            venteEntity.ClientsId = venteWriteDto.ClientsId;
            venteEntity.IdUserEnr = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            venteEntity.LibelleFacture = $"Reservation Client : {User.FindFirst("Name").Value} {User.FindFirst("").Value} ";
            venteEntity.MontantTotal = venteEntity.VentProds.Sum(x => x.PrixVente * x.QteVendu - x.MntRemise);

            await _repository.Vente.UpdateVenteAsync(venteEntity);
            await _repository.SaveAsync();

            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVente(Guid id)
        {
            var vente = await _repository.Vente.GetVenteByIdAsync(id);

            if (vente == null)
            {
                _logger.LogError($"Vente with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Vente.DeleteVenteAsync(vente);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
