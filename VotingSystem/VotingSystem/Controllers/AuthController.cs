using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using VotingSystem.Dtos;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{
    [Authorize(Roles = UserRoleNames.Admin)]
    [ApiController]
    [Route("users")]
    public class AuthController : ControllerBase
    {
        private readonly IVotingSystemDb _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _appConfig;
        public AuthController(IVotingSystemDb db, IMapper mapper, IConfiguration appConfig)
        {
            _db = db;
            _mapper = mapper;
            _appConfig = appConfig;
        }

        [HttpGet]
        public ActionResult<List<UserReadDto>> GetUsers()
        {
            var users = _db.AuthRepo.GetAllUsers();
            
            var outputDtos = _mapper.Map<IEnumerable<UserReadDto>>(users);

            return Ok(outputDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<UserReadDto> GetUser(int id)
        {
            var existingUser = _db.AuthRepo.FindUserById(id);
            if (existingUser == null)
                return NotFound();

            var outputDto = _mapper.Map<UserReadDto>(existingUser);

            return Ok(outputDto);
        }

        [HttpPost]
        public ActionResult<UserReadDto> RegisterUser(UserRegisterDto inputDto)
        {
            try
            {
                var newUser = Models.User.FromCredentials(
                    inputDto.Username,
                    inputDto.Password,
                    inputDto.RoleId);

                _db.AuthRepo.AddUser(newUser);
                _db.SaveChanges();

                var outputDto = _mapper.Map<UserReadDto>(newUser);
                return CreatedAtAction(
                    actionName: nameof(GetUser),
                    routeValues: new { id = outputDto.Id },
                    value: outputDto);
            }
            catch(ArgumentException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(ex.Message);
            }
            
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserLoginSuccessDto> LoginUser(UserLoginAttemptDto inputDto)
        {
            var existingUser = _db.AuthRepo.FindUserByUsername(inputDto.Username);
            if (existingUser == null)
                return ValidationProblem();

            var authResult = existingUser.Authenticate(inputDto.Password);
            if (!authResult)
                return ValidationProblem();

            var accessToken = AccessToken.ForUser(existingUser, _appConfig["AppSecret"]);

            return new UserLoginSuccessDto 
            { 
                AccessToken = accessToken.TokenString,
                Username = existingUser.Username,
                Role = existingUser.Role.Name
            };
        }
        
        [Authorize(Roles = UserRoleNames.Admin)]
        [HttpPut("{userId}/status")]
        public ActionResult SetUserLockedStatus(long userId, SetUserLockedStatusDto inputDto)
        {
            var existingUser = _db.AuthRepo.FindUserById(userId);
            if (existingUser == null)
                return NotFound();

            existingUser.Locked = inputDto.Locked;
            _db.SaveChanges();

            return Ok();
        }
    }
}