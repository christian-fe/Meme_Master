using AutoMapper;//*
using Meme.Models;//*
using Meme.Models.Dto;//*
using Meme.Repository.IRepository;//*
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Meme.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _ctRepo;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
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


        [HttpGet("{categoryId:int}", Name = "GetCategory")]
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


        [HttpPost]
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

        [HttpPatch("{categoryId:int}", Name = "UpdateCategory")]
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

        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
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
