﻿using AutoMapper;//*
using Meme.Models;//*
using Meme.Models.Dto;//*
using Meme.Repository.IRepository;//*
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                ModelState.AddModelError("", "The category is already exist");
                return StatusCode(404, ModelState);
            }

            var category = _mapper.Map<Category>(categoryDto);

            //validate bad request
            if(!_ctRepo.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Something is wrong saving the category {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = category.IdCategory },category);
        }
    }
}
