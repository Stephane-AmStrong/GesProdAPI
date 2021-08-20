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
    public class ClientsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ClientsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClients([FromQuery] PaginationParameters paginationParameters)
        {
            var clients = await _repository.Client.GetAllClientsAsync(paginationParameters);

            var metadata = new
            {
                clients.TotalCount,
                clients.PageSize,
                clients.CurrentPage,
                clients.TotalPages,
                clients.HasNext,
                clients.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all clients clients from database.");

            var clientsReadDto = _mapper.Map<IEnumerable<ClientReadDto>>(clients);

            return Ok(clientsReadDto);
        }



        [HttpGet("{id}", Name = "ClientById")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var client = await _repository.Client.GetClientByIdAsync(id);

            if (client == null)
            {
                _logger.LogError($"Client with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned clientWriteDto with id: {id}");

                var clientReadDto = _mapper.Map<ClientReadDto>(client);
                
                return Ok(clientReadDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientWriteDto client)
        {
            if (client == null)
            {
                _logger.LogError("Client object sent from client is null.");
                return BadRequest("Client object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid clientWriteDto object sent from client.");
                return BadRequest("Invalid model object");
            }

            var clientEntity = _mapper.Map<Client>(client);
            clientEntity.Id = Guid.NewGuid();

            if (await _repository.Client.ClientExistAsync(clientEntity))
            {
                ModelState.AddModelError("", "This Client exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Client.CreateClientAsync(clientEntity);
            await _repository.SaveAsync();

            var clientReadDto = _mapper.Map<ClientReadDto>(clientEntity);
            return CreatedAtRoute("ClientById", new { id = clientReadDto.Id }, clientReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] ClientWriteDto clientWriteDto)
        {
            if (clientWriteDto == null)
            {
                _logger.LogError("Client object sent from client is null.");
                return BadRequest("Client object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid clientWriteDto object sent from client.");
                return BadRequest("Invalid model object");
            }

            var clientEntity = await _repository.Client.GetClientByIdAsync(id);
            if (clientEntity == null)
            {
                _logger.LogError($"Client with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(clientWriteDto, clientEntity);


            await _repository.Client.UpdateClientAsync(clientEntity);
            await _repository.SaveAsync();

            var clientReadDto = _mapper.Map<ClientReadDto>(clientEntity);
            return Ok(clientReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = await _repository.Client.GetClientByIdAsync(id);

            if (client == null)
            {
                _logger.LogError($"Client with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Client.DeleteClientAsync(client);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
