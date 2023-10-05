using Azure;
using BusinessLayer.BL_layer;
using BusinessLayer.RespositoryLayer;
using CRUD.Utils;
using CrudValidation.LoginValidation;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.Controllers
{
    [Route("Auth/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<Register> _registerValidator;
        private readonly IValidator<User> _userValidator;

        public LoginController(
            IAuthenticationService authenticationService,
            IValidator<Register> validator,
            IValidator<User> _userValidator)
        {
           this._authenticationService = authenticationService;
           this._registerValidator = validator;
           this._userValidator = _userValidator;
        }

        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
           return "value";
        }

        ///POST api/<LoginController>
        [HttpPost]
        public async Task<Responses> SignIn([FromBody] User user)
        {
            var loginValidaion = await _userValidator.ValidateAsync(user);
            if (loginValidaion.IsValid)
            {
                var result = await _authenticationService.SignIn(user);
                if (result is not null)
                {
                    return StatusHandler.ProcessHttpStatusCode(result, "Login SuccessFully");
                }
                else
                {
                    return StatusHandler.ProcessHttpStatusCode(null, "Email or Password InCorrect");
                }
            }
            else
            {
                List<String> errors = new List<String>();
                foreach (var key in loginValidaion.Errors)
                {
                    errors.Add(key.ErrorMessage);
                }
                return StatusHandler.ProcessHttpStatusCode(errors, "Validation Fails");
            }
        }

        [HttpPost("SignUp")]
        public async Task<Responses> SignUp([FromBody] Register register)
        {
            var registerValidaion = await _registerValidator.ValidateAsync(register);
            if (registerValidaion.IsValid)
            {
                var result = await _authenticationService.SignUp(register);
                if (result)
                {
                    return StatusHandler.ProcessHttpStatusCode(register, "Registered SuccessFully");
                }
                else
                {
                    return StatusHandler.ProcessHttpStatusCode(null, "Internall Server Error");
                }
            }
            else
            {
                List<String> errors = new List<String>();
                foreach (var key in registerValidaion.Errors)
                {
                   errors.Add(key.ErrorMessage);
                }
                return StatusHandler.ProcessHttpStatusCode(errors, "Validation Fails");
            }
        }


        [HttpPost("RefreshToken")]
        public async Task<Responses> Refresh([FromBody] UserTokens tokenApiDto)
        {
            var tokenGenrated = await _authenticationService.UpdateRefreshTokens(tokenApiDto?.RefreshTokens, tokenApiDto?.JwtTokens);
            if(tokenGenrated is not null)
            {
                return StatusHandler.ProcessHttpStatusCode(tokenGenrated,"TokenGenerated Succesfully");
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(tokenGenrated, "Token is Invalid to process or Token Expired");
            }
         
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
