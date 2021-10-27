using AutoMapper;//*
using Memes.Models;//*
using Memes.Models.Dto;//*
using Memes.Repository.IRepository;//*
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;//*
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Meme.Controllers
{
    [Authorize]
    [Route("api/Photos")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Api-Photo")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PhotosController : Controller
    {
        private readonly IPhotoRepository _phRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnviroment;
        public PhotosController(IPhotoRepository phRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _phRepo = phRepo;
            _mapper = mapper;
            _hostingEnviroment = hostingEnvironment;
        }

        /// <summary>
        /// Get all the photos
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PhotoDto>))]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Get photo by Id
        /// </summary>
        /// <param name="photoId">int Id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{photoId:int}", Name = "GetPhoto")]
        [ProducesResponseType(200, Type = typeof(PhotoDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Get the photos by category
        /// </summary>
        /// <param name="idCategory">int Id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetPhotoByCategory/{idCategory:int}")]
        [ProducesResponseType(200, Type = typeof(PhotoDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPhotoByCategory(int idCategory)
        {
            var photoList = _phRepo.GetPhotoByCategory(idCategory);

            //validate if is null
            if(photoList == null)
            {
                return NotFound();
            }

            var photoListDto = new List<PhotoDto>();

            foreach(var photo in photoList)
            {
                photoListDto.Add(_mapper.Map<PhotoDto>(photo));
            }

            return Ok(photoListDto);
        }

        /// <summary>
        /// Get photos by name
        /// </summary>
        /// <param name="photoName">string meme name</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetPhotoByName")]
        [ProducesResponseType(200, Type = typeof(PhotoDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPhotoByName(string photoName)
        {
            try
            {
                var result = _phRepo.GetPhotoByName(photoName);
                if (result.Any()) 
                { 
                    return Ok(result); 
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving information from the database");
            }
        }

        /// <summary>
        /// Make a new meme uploading photos
        /// </summary>
        /// <param name="photoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePhoto([FromForm] PhotoCreateDto photoDto)
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

            /*------------------uploading files------------------------*/

            var file = photoDto.Photo;
            string principalPath = _hostingEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (file.Length > 0)
            {
                //new photo or image
                var photoName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(principalPath, @"photos");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, photoName + extension), FileMode.Create))
                { 
                    files[0].CopyTo(fileStreams);
                } 

                photoDto.ImagePath = @"\photos\" + photoName + extension;
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

        /// <summary>
        /// Upadate a photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="photoDto"></param>
        /// <returns></returns>
        [HttpPatch("{photoId:int}", Name = "UpdatePhoto")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Delete a photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [HttpDelete("{photoId:int}", Name = "DeletePhoto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
