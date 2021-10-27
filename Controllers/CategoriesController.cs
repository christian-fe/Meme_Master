using AutoMapper;//*
using Memes.Models;//*
using Memes.Models.Dto;//*
using Memes.Repository.IRepository;//*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Memes.Controllers
{
    [Authorize]
    [Route("api/Categories")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Meme_Master")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _ctRepo;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// To obtain all the categories
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<CategoryDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories()
        {
            var categoriesList = _ctRepo.GetCategories();

            var categoriesDtoList = new List<CategoryDto>();

            foreach(var item in categoriesList)
            {
                categoriesDtoList.Add(_mapper.Map<CategoryDto>(item));
            }

            return Ok(categoriesDtoList);
        }

        /// <summary>
        /// To obtain only one category
        /// </summary>
        /// <param name="categoryId">this is the Id category</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetCategory(int categoryId)
        {
            var itemCategory = _ctRepo.GetCategory(categoryId);

            if (itemCategory == null)
            {
                return NotFound();
            }
            else
            {
                var itemCategoryDto = _mapper.Map<CategoryDto>(itemCategory);
                return Ok(itemCategoryDto);
            }

        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            //validate is not null
            if(categoryDto ==null)
            {
                return BadRequest(ModelState);
            }

            //validate if exist
            if(_ctRepo.ExistCategory(categoryDto.CategoryName))
            {
                ModelState.AddModelError("", "The category already exists");
                return StatusCode(404, ModelState);
            }

            var category = _mapper.Map<Category>(categoryDto);

            //validate bad request
            if(!_ctRepo.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the category {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = category.IdCategory },category);
        }

        /// <summary>
        /// Update an existent category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPatch("{categoryId:int}", Name = "UpdateCategory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int categoryId, [FromBody]CategoryDto categoryDto)
        {
            //validate is not null or Id doesn't match
            if(categoryDto==null || categoryId != categoryDto.IdCategory)
            {
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(categoryDto);

            //validate bad request
            if (!_ctRepo.UpdateCategory(category))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the category {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Delete an specific category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int categoryId)
        {

            //var category = _mapper.Map<Category>(categoryDto);

            //validate if exist
            if (!_ctRepo.ExistCategory(categoryId))
            {
                return NotFound();
            }

            var category = _ctRepo.GetCategory(categoryId);

            //validate bad request
            if (!_ctRepo.DeleteCategory(category))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the category {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
