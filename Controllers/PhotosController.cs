using AutoMapper;//*
using Memes.Models;//*
using Memes.Models.Dto;//*
using Memes.Repository.IRepository;//*
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme.Controllers
{
    [Route("api/Photos")]
    [ApiController]
    public class PhotosController : Controller
    {
        private readonly IPhotoRepository _phRepo;
        private readonly IMapper _mapper;
        public PhotosController(IPhotoRepository phRepo, IMapper mapper)
        {
            _phRepo = phRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPhotos()
        {
            var photosList = _phRepo.GetPhoto();

            var photosDtoList = new List<PhotoDto>();

            foreach (var item in photosList)
            {
                photosDtoList.Add(_mapper.Map<PhotoDto>(item));
            }

            return Ok(photosDtoList);
        }

        [HttpGet("{photoId:int}", Name = "GetPhoto")]
        public IActionResult GetPhoto(int photoId)
        {
            var itemPhoto = _phRepo.GetPhoto(photoId);

            if (itemPhoto == null)
            {
                return NotFound();
            }
            else
            {
                var itemPhotoDto = _mapper.Map<PhotoDto>(itemPhoto);
                return Ok(itemPhotoDto);
            }

        }

        [HttpPost]
        public IActionResult CreatePhoto([FromBody] PhotoDto photoDto)
        {
            //validate is not null
            if (photoDto == null)
            {
                return BadRequest(ModelState);
            }

            //validate if exist
            if (_phRepo.ExistPhoto(photoDto.PhotoName))
            {
                ModelState.AddModelError("", "The Photo already exists");
                return StatusCode(404, ModelState);
            }

            var photo = _mapper.Map<Photo>(photoDto);

            //validate bad request
            if (!_phRepo.CreatePhoto(photo))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the Photo {photo.PhotoName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPhoto", new { photoId = photo.PhotoId }, photo);
        }

        [HttpPatch("{photoId:int}", Name = "UpdatePhoto")]
        public IActionResult UpdatePhoto(int photoId, [FromBody] PhotoDto photoDto)
        {
            //validate is not null or Id doesn't match
            if (photoDto == null || photoId != photoDto.PhotoId)
            {
                return BadRequest(ModelState);
            }

            var photo = _mapper.Map<Photo>(photoDto);

            //validate bad request
            if (!_phRepo.UpdatePhoto(photo))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the Photo {photo.PhotoName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{photoId:int}", Name = "DeletePhoto")]
        public IActionResult DeletePhoto(int photoId)
        {

            //var Photo = _mapper.Map<Photo>(PhotoDto);

            //validate if exist
            if (!_phRepo.ExistPhoto(photoId))
            {
                return NotFound();
            }

            var photo = _phRepo.GetPhoto(photoId);

            //validate bad request
            if (!_phRepo.DeletePhoto(photo))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the Photo {photo.PhotoName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
