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
    public class CategoriesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParameters paginationParameters)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(paginationParameters);

            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned all categories categories from database.");

            var categoriesReadDto = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

            return Ok(categoriesReadDto);
        }



        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(id);

            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Returned categoryWriteDto with id: {id}");

                var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
                
                return Ok(categoryReadDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryWriteDto category)
        {
            if (category == null)
            {
                _logger.LogError("Category object sent from category is null.");
                return BadRequest("Category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid categoryWriteDto object sent from category.");
                return BadRequest("Invalid model object");
            }

            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.Id = Guid.NewGuid();


            if (await _repository.Category.CategoryExistAsync(categoryEntity))
            {
                ModelState.AddModelError("", "This Category exists already");
                return ValidationProblem(ModelState);
            }


            await _repository.Category.CreateCategoryAsync(categoryEntity);
            await _repository.SaveAsync();

            var categoryReadDto = _mapper.Map<CategoryReadDto>(categoryEntity);
            return CreatedAtRoute("CategoryById", new { id = categoryReadDto.Id }, categoryReadDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryWriteDto categoryWriteDto)
        {
            if (categoryWriteDto == null)
            {
                _logger.LogError("Category object sent from category is null.");
                return BadRequest("Category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid categoryWriteDto object sent from category.");
                return BadRequest("Invalid model object");
            }

            var categoryEntity = await _repository.Category.GetCategoryByIdAsync(id);
            if (categoryEntity == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found.");
                return NotFound();
            }

            _mapper.Map(categoryWriteDto, categoryEntity);


            await _repository.Category.UpdateCategoryAsync(categoryEntity);
            await _repository.SaveAsync();

            var categoryReadDto = _mapper.Map<CategoryReadDto>(categoryEntity);
            return Ok(categoryReadDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(id);

            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found.");
                return NotFound();
            }


            await _repository.Category.DeleteCategoryAsync(category);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
