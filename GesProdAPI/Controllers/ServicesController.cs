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
    public class ServicesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly string _baseURL;

        public ServicesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _repository.FolderName = "Service";
            _baseURL = string.Concat(httpContextAccessor.HttpContext.Request.Scheme, "://", httpContextAccessor.HttpContext.Request.Host);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllServices([FromQuery] PaginationParameters paginationParameters)
        {
            var services = await _repository.Service.GetAllServicesAsync(paginationParameters);

            var metadata = new
            {
                services.TotalCount,
                services.PageSize,
                services.CurrentPage,
                services.TotalPages,
                services.HasNext,
                services.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all services services from database.");

            var servicesReadDto = _mapper.Map<IEnumerable<ServiceReadDto>>(services);

            servicesReadDto.ToList().ForEach(serviceReadDto =>
            {
                if (!string.IsNullOrWhiteSpace(serviceReadDto.Photo)) serviceReadDto.Photo = $"{_baseURL}{serviceReadDto.Photo.Replace("~", "")}";
            });

            return Ok(servicesReadDto);
        }



        [HttpGet("{id}", Name = "ServiceById")]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            var service = await _repository.Service.GetServiceByIdAsync(id);

            if (service == null)
            {
                _logger.LogError($"Service with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned serviceWriteDto with id: {id}");

                var serviceReadDto = _mapper.Map<ServiceReadDto>(service);

                if (!string.IsNullOrWhiteSpace(serviceReadDto.Photo)) serviceReadDto.Photo = $"{_baseURL}{serviceReadDto.Photo.Replace("~", "")}";

                return Ok(serviceReadDto);
            }
        }



        [HttpGet("available/from/{debutPeriode}/to/{finPeriode}")]
        public async Task<IActionResult> GetServiceDisponible(DateTime debutPeriode, DateTime finPeriode)
        {
            var servicesDisponible = await _repository.Service.GetAvailableServicesAsync(debutPeriode, finPeriode);

            var servicesReadDto = _mapper.Map<IEnumerable<ServiceReadDto>>(servicesDisponible);

            servicesReadDto.ToList().ForEach(serviceReadDto =>
            {
                if (!string.IsNullOrWhiteSpace(serviceReadDto.Photo)) serviceReadDto.Photo = $"{_baseURL}{serviceReadDto.Photo.Replace("~", "")}";
            });

            return Ok(servicesReadDto);

        }





        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceWriteDto service)
        {
            if (service == null)
            {
                _logger.LogError("Service object sent from service is null.");
                return BadRequest("Service object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid serviceWriteDto object sent from service.");
                return BadRequest("Invalid model object");
            }

            var serviceEntity = _mapper.Map<Service>(service);
            serviceEntity.Id = Guid.NewGuid();

            if (await _repository.Service.ServiceExistAsync(serviceEntity))
            {
                ModelState.AddModelError("", "This Service exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Service.CreateServiceAsync(serviceEntity);
            await _repository.SaveAsync();

            var serviceReadDto = _mapper.Map<ServiceReadDto>(serviceEntity);
            return CreatedAtRoute("ServiceById", new { id = serviceReadDto.Id }, serviceReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceWriteDto serviceWriteDto)
        {
            if (serviceWriteDto == null)
            {
                _logger.LogError("Service object sent from service is null.");
                return BadRequest("Service object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid serviceWriteDto object sent from service.");
                return BadRequest("Invalid model object");
            }

            var serviceEntity = await _repository.Service.GetServiceByIdAsync(id);
            if (serviceEntity == null)
            {
                _logger.LogError($"Service with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(serviceWriteDto, serviceEntity);


            await _repository.Service.UpdateServiceAsync(serviceEntity);
            await _repository.SaveAsync();

            var serviceReadDto = _mapper.Map<ServiceReadDto>(serviceEntity);

            return Ok(serviceReadDto);
        }



        [HttpPut("{id}/upload-picture")]
        public async Task<ActionResult<ServiceReadDto>> UploadPicture(Guid id, [FromForm] IFormFile picture)
        {
            var serviceEntity = await _repository.Service.GetServiceByIdAsync(id);
            if (serviceEntity == null) return NotFound();

            if (picture != null)
            {
                _repository.Image.PictureName = id.ToString();

                var uploadResult = await _repository.Image.UploadImage(picture);

                if (uploadResult == null)
                {
                    ModelState.AddModelError("", "something went wrong when uploading the picture");
                    return ValidationProblem(ModelState);
                }
                else
                {
                    serviceEntity.Photo = uploadResult;
                }
            }

            await _repository.Service.UpdateServiceAsync(serviceEntity);

            await _repository.SaveAsync();

            var serviceReadDto = _mapper.Map<ServiceReadDto>(serviceEntity);

            if (!string.IsNullOrWhiteSpace(serviceReadDto.Photo)) serviceReadDto.Photo = $"{_baseURL}{serviceReadDto.Photo}";

            return Ok(serviceReadDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _repository.Service.GetServiceByIdAsync(id);

            if (service == null)
            {
                _logger.LogError($"Service with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Service.DeleteServiceAsync(service);

            await _repository.SaveAsync();

            return NoContent();
        }



    }
}
