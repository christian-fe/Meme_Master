using AutoMapper;//*
using Memes.Models;//*
using Memes.Models.Dto;//*
using Memes.Repository.IRepository;//*
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
    [Route("api/Users")]
    [ApiController]
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

        [HttpGet]
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

        [HttpGet("{Id:int}", Name = "GetUser")]
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

        [HttpPost("Registry")]
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

        [HttpPost("Login")]
        public IActionResult Login(UserAuthLoginDto userAuthLoginDto)
        {
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
