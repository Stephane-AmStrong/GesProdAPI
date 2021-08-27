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
    public class SitesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public SitesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSites([FromQuery] PaginationParameters paginationParameters)
        {
            var sites = await _repository.Site.GetAllSitesAsync(paginationParameters);

            var metadata = new
            {
                sites.TotalCount,
                sites.PageSize,
                sites.CurrentPage,
                sites.TotalPages,
                sites.HasNext,
                sites.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all sites from database.");

            var sitesReadDto = _mapper.Map<IEnumerable<SiteReadDto>>(sites);

            return Ok(sitesReadDto);
        }



        [HttpGet("{id}", Name = "SiteById")]
        public async Task<IActionResult> GetSiteById(Guid id)
        {
            var site = await _repository.Site.GetSiteByIdAsync(id);

            if (site == null)
            {
                _logger.LogError($"Site with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned siteWriteDto with id: {id}");

                var siteReadDto = _mapper.Map<SiteReadDto>(site);
                
                return Ok(siteReadDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateSite([FromBody] SiteWriteDto site)
        {
            if (site == null)
            {
                _logger.LogError("Site object sent from site is null.");
                return BadRequest("Site object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid siteWriteDto object sent from site.");
                return BadRequest("Invalid model object");
            }

            var siteEntity = _mapper.Map<Site>(site);
            siteEntity.Id = Guid.NewGuid();

            if (await _repository.Site.SiteExistAsync(siteEntity))
            {
                ModelState.AddModelError("", "This Site exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Site.CreateSiteAsync(siteEntity);
            await _repository.SaveAsync();

            var siteReadDto = _mapper.Map<SiteReadDto>(siteEntity);
            return CreatedAtRoute("SiteById", new { id = siteReadDto.Id }, siteReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSite(Guid id, [FromBody] SiteWriteDto siteWriteDto)
        {
            if (siteWriteDto == null)
            {
                _logger.LogError("Site object sent from site is null.");
                return BadRequest("Site object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid siteWriteDto object sent from site.");
                return BadRequest("Invalid model object");
            }

            var siteEntity = await _repository.Site.GetSiteByIdAsync(id);
            if (siteEntity == null)
            {
                _logger.LogError($"Site with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(siteWriteDto, siteEntity);


            await _repository.Site.UpdateSiteAsync(siteEntity);
            await _repository.SaveAsync();

            var siteReadDto = _mapper.Map<SiteReadDto>(siteEntity);
            return Ok(siteReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(Guid id)
        {
            var site = await _repository.Site.GetSiteByIdAsync(id);

            if (site == null)
            {
                _logger.LogError($"Site with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Site.DeleteSiteAsync(site);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
