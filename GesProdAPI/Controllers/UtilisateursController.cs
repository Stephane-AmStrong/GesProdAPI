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
    public class UtilisateursController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UtilisateursController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUtilisateurs([FromQuery] PaginationParameters paginationParameters)
        {
            var utilisateurs = await _repository.Authentication.GetAllUsersAsync(paginationParameters);

            var metadata = new
            {
                utilisateurs.TotalCount,
                utilisateurs.PageSize,
                utilisateurs.CurrentPage,
                utilisateurs.TotalPages,
                utilisateurs.HasNext,
                utilisateurs.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all utilisateurs utilisateurs from database.");

            var utilisateursReadDto = _mapper.Map<IEnumerable<AuthUserReadDto>>(utilisateurs);

            return Ok(utilisateursReadDto);
        }



        [HttpGet("{id}", Name = "UtilisateurById")]
        public async Task<IActionResult> GetUtilisateurById(Guid id)
        {
            var utilisateur = await _repository.Utilisateur.GetUtilisateurByIdAsync(id);

            if (utilisateur == null)
            {
                _logger.LogError($"Utilisateur with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned utilisateurWriteDto with id: {id}");

                var utilisateurReadDto = _mapper.Map<UtilisateurReadDto>(utilisateur);
                return Ok(utilisateurReadDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateUtilisateur([FromBody] UtilisateurWriteDto utilisateur)
        {
            if (utilisateur == null)
            {
                _logger.LogError("Utilisateur object sent from utilisateur is null.");
                return BadRequest("Utilisateur object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid utilisateurWriteDto object sent from utilisateur.");
                return BadRequest("Invalid model object");
            }

            var utilisateurEntity = _mapper.Map<Utilisateur>(utilisateur);
            utilisateurEntity.Id = Guid.NewGuid();

            if (await _repository.Utilisateur.UtilisateurExistAsync(utilisateurEntity))
            {
                ModelState.AddModelError("", "This Utilisateur exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Utilisateur.CreateUtilisateurAsync(utilisateurEntity);
            await _repository.SaveAsync();

            var utilisateurReadDto = _mapper.Map<UtilisateurReadDto>(utilisateurEntity);
            return CreatedAtRoute("UtilisateurById", new { id = utilisateurReadDto.Id }, utilisateurReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(Guid id, [FromBody] UtilisateurWriteDto utilisateurWriteDto)
        {
            if (utilisateurWriteDto == null)
            {
                _logger.LogError("Utilisateur object sent from utilisateur is null.");
                return BadRequest("Utilisateur object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid utilisateurWriteDto object sent from utilisateur.");
                return BadRequest("Invalid model object");
            }

            var utilisateurEntity = await _repository.Utilisateur.GetUtilisateurByIdAsync(id);
            if (utilisateurEntity == null)
            {
                _logger.LogError($"Utilisateur with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(utilisateurWriteDto, utilisateurEntity);


            await _repository.Utilisateur.UpdateUtilisateurAsync(utilisateurEntity);
            await _repository.SaveAsync();

            var utilisateurReadDto = _mapper.Map<UtilisateurReadDto>(utilisateurEntity);
            return Ok(utilisateurReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(Guid id)
        {
            var utilisateur = await _repository.Utilisateur.GetUtilisateurByIdAsync(id);

            if (utilisateur == null)
            {
                _logger.LogError($"Utilisateur with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Utilisateur.DeleteUtilisateurAsync(utilisateur);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
