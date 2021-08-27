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
    public class ProfilsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProfilsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProfils([FromQuery] PaginationParameters paginationParameters)
        {
            var profils = await _repository.Profil.GetAllProfilsAsync(paginationParameters);

            var metadata = new
            {
                profils.TotalCount,
                profils.PageSize,
                profils.CurrentPage,
                profils.TotalPages,
                profils.HasNext,
                profils.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all profils from database.");

            var profilsReadDto = _mapper.Map<IEnumerable<ProfilReadDto>>(profils);

            return Ok(profilsReadDto);
        }



        [HttpGet("{id}", Name = "ProfilById")]
        public async Task<IActionResult> GetProfilById(Guid id)
        {
            var profil = await _repository.Profil.GetProfilByIdAsync(id);

            if (profil == null)
            {
                _logger.LogError($"Profil with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned profilWriteDto with id: {id}");

                var profilReadDto = _mapper.Map<ProfilReadDto>(profil);
                
                return Ok(profilReadDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateProfil([FromBody] ProfilWriteDto profil)
        {
            if (profil == null)
            {
                _logger.LogError("Profil object sent from profil is null.");
                return BadRequest("Profil object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid profilWriteDto object sent from profil.");
                return BadRequest("Invalid model object");
            }

            var profilEntity = _mapper.Map<Profil>(profil);
            profilEntity.Id = Guid.NewGuid();

            if (await _repository.Profil.ProfilExistAsync(profilEntity))
            {
                ModelState.AddModelError("", "This Profil exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Profil.CreateProfilAsync(profilEntity);
            await _repository.SaveAsync();

            var profilReadDto = _mapper.Map<ProfilReadDto>(profilEntity);
            return CreatedAtRoute("ProfilById", new { id = profilReadDto.Id }, profilReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfil(Guid id, [FromBody] ProfilWriteDto profilWriteDto)
        {
            if (profilWriteDto == null)
            {
                _logger.LogError("Profil object sent from profil is null.");
                return BadRequest("Profil object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid profilWriteDto object sent from profil.");
                return BadRequest("Invalid model object");
            }

            var profilEntity = await _repository.Profil.GetProfilByIdAsync(id);
            if (profilEntity == null)
            {
                _logger.LogError($"Profil with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(profilWriteDto, profilEntity);


            await _repository.Profil.UpdateProfilAsync(profilEntity);
            await _repository.SaveAsync();

            var profilReadDto = _mapper.Map<ProfilReadDto>(profilEntity);
            return Ok(profilReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(Guid id)
        {
            var profil = await _repository.Profil.GetProfilByIdAsync(id);

            if (profil == null)
            {
                _logger.LogError($"Profil with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Profil.DeleteProfilAsync(profil);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
