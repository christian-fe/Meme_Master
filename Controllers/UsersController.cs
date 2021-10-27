using AutoMapper;//*
using Memes.Models;//*
using Memes.Models.Dto;//*
using Memes.Repository.IRepository;//*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Memes.Controllers
{
    [Authorize]
    [Route("api/Users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Api-User")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersController : Controller
    {
        private readonly IUserRepository _usRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UsersController(IUserRepository usRepo, IMapper mapper, IConfiguration config)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            _config = config;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var usersList = _usRepo.GetUsers();

            var usersDtoList = new List<UserDto>();

            foreach(var item in usersList)
            {
                usersDtoList.Add(_mapper.Map<UserDto>(item));
            }

            return Ok(usersDtoList);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns></returns>
        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int Id)
        {
            var userItem = _usRepo.GetUser(Id);

            //if is null
            if (userItem == null)
            {
                return NotFound();
            }
            else
            {
                var userItemDto = _mapper.Map<UserDto>(userItem);
                return Ok(userItemDto);
            }

        }

        /// <summary>
        /// Regitry for new users
        /// </summary>
        /// <param name="userAuthDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Registry")]
        [ProducesResponseType(201, Type = typeof(UserAuthDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Registry(UserAuthDto userAuthDto)
        {
            userAuthDto.User = userAuthDto.User.ToLower();

            //validate is not null
            if (userAuthDto == null)
            {
                return BadRequest(ModelState);
            }

            //validate if exist
            if(_usRepo.ExistUser(userAuthDto.User))
            {                
                return BadRequest("The user already exists");
            }

            var userToCreate = new User
            {
                UserA = userAuthDto.User
            };

            var createdUser = _usRepo.Registry(userToCreate, userAuthDto.Password);
            return Ok(createdUser);

        }

        /// <summary>
        /// Perform a Log In
        /// </summary>
        /// <param name="userAuthLoginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(201, Type = typeof(UserAuthLoginDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(UserAuthLoginDto userAuthLoginDto)
        {
            //throw new Exception("generated error");

            var userFromRepo = _usRepo.Login(userAuthLoginDto.User, userAuthLoginDto.Password);

            if(userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserA.ToString())
            };

            // Token Generation
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { 
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
