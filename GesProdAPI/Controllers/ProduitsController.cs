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
    public class ProduitsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly string _baseURL;

        public ProduitsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _repository.FolderName = "Produit";
            _baseURL = string.Concat(httpContextAccessor.HttpContext.Request.Scheme, "://", httpContextAccessor.HttpContext.Request.Host);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProduits([FromQuery] PaginationParameters paginationParameters)
        {
            var produits = await _repository.Produit.GetAllProduitsAsync(paginationParameters);

            var metadata = new
            {
                produits.TotalCount,
                produits.PageSize,
                produits.CurrentPage,
                produits.TotalPages,
                produits.HasNext,
                produits.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all produits produits from database.");

            var produitsReadDto = _mapper.Map<IEnumerable<ProduitReadDto>>(produits);

            produitsReadDto.ToList().ForEach(produitReadDto =>
            {
                if (!string.IsNullOrWhiteSpace(produitReadDto.Photo)) produitReadDto.Photo = $"{_baseURL}{produitReadDto.Photo.Replace("~","")}";
            });

            return Ok(produitsReadDto);
        }



        [HttpGet("{id}", Name = "ProduitById")]
        public async Task<IActionResult> GetProduitById(Guid id)
        {
            var produit = await _repository.Produit.GetProduitByIdAsync(id);

            if (produit == null)
            {
                _logger.LogError($"Produit with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned produitWriteDto with id: {id}");

                var produitReadDto = _mapper.Map<ProduitReadDto>(produit);

                if (!string.IsNullOrWhiteSpace(produitReadDto.Photo)) produitReadDto.Photo = $"{_baseURL}{produitReadDto.Photo.Replace("~", "")}";

                return Ok(produitReadDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateProduit([FromBody] ProduitWriteDto produit)
        {
            if (produit == null)
            {
                _logger.LogError("Produit object sent from produit is null.");
                return BadRequest("Produit object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid produitWriteDto object sent from produit.");
                return BadRequest("Invalid model object");
            }

            var produitEntity = _mapper.Map<Produit>(produit);
            produitEntity.Id = Guid.NewGuid();

            if (await _repository.Produit.ProduitExistAsync(produitEntity))
            {
                ModelState.AddModelError("", "This Produit exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Produit.CreateProduitAsync(produitEntity);
            await _repository.SaveAsync();

            var produitReadDto = _mapper.Map<ProduitReadDto>(produitEntity);
            return CreatedAtRoute("ProduitById", new { id = produitReadDto.Id }, produitReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduit(Guid id, [FromBody] ProduitWriteDto produitWriteDto)
        {
            if (produitWriteDto == null)
            {
                _logger.LogError("Produit object sent from produit is null.");
                return BadRequest("Produit object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid produitWriteDto object sent from produit.");
                return BadRequest("Invalid model object");
            }

            var produitEntity = await _repository.Produit.GetProduitByIdAsync(id);
            if (produitEntity == null)
            {
                _logger.LogError($"Produit with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(produitWriteDto, produitEntity);


            await _repository.Produit.UpdateProduitAsync(produitEntity);
            await _repository.SaveAsync();

            var produitReadDto = _mapper.Map<ProduitReadDto>(produitEntity);

            return Ok(produitReadDto);
        }


        [HttpPut("{id}/upload-picture")]
        public async Task<ActionResult<ProduitReadDto>> UploadPicture(Guid id, [FromForm] IFormFile picture)
        {
            var produitEntity = await _repository.Produit.GetProduitByIdAsync(id);
            if (produitEntity == null) return NotFound();

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
                    produitEntity.Photo = uploadResult;
                }
            }

            await _repository.Produit.UpdateProduitAsync(produitEntity);

            await _repository.SaveAsync();

            var produitReadDto = _mapper.Map<ProduitReadDto>(produitEntity);

            if (!string.IsNullOrWhiteSpace(produitReadDto.Photo)) produitReadDto.Photo = $"{_baseURL}{produitReadDto.Photo}";

            return Ok(produitReadDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(Guid id)
        {
            var produit = await _repository.Produit.GetProduitByIdAsync(id);

            if (produit == null)
            {
                _logger.LogError($"Produit with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Produit.DeleteProduitAsync(produit);

            await _repository.SaveAsync();

            return NoContent();
        }



    }
}
