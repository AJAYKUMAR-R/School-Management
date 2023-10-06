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
                    return StatusHandler.ProcessHttpStatusCode(result, "Login SuccessFully",200);
                }
                else
                {
                    List<string> errors = new List<string>() {
                        "Username or Password is in Correct"
                    };
                     
                    return StatusHandler.ProcessHttpStatusCode(errors, "Email or Password InCorrect",400);
                }
            }
            else
            {
                List<String> errors = new List<String>();
                foreach (var key in loginValidaion.Errors)
                {
                    errors.Add(key.ErrorMessage);
                }
                return StatusHandler.ProcessHttpStatusCode(errors, "Validation Fails", 400);
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
                    return StatusHandler.ProcessHttpStatusCode(register, "Registered SuccessFully", 200);
                }
                else
                {
                    return StatusHandler.ProcessHttpStatusCode(null, "Internall Server Error",500);
                }
            }
            else
            {
                List<String> errors = new List<String>();
                foreach (var key in registerValidaion.Errors)
                {
                   errors.Add(key.ErrorMessage);
                }
                return StatusHandler.ProcessHttpStatusCode(errors, "Validation Fails",400);
            }
        }


        [HttpPost("RefreshToken")]
        public async Task<Responses> Refresh([FromBody] UserTokens tokenApiDto)
        {
            var tokenGenrated = await _authenticationService.UpdateRefreshTokens(tokenApiDto?.RefreshTokens, tokenApiDto?.JwtTokens);
            if(tokenGenrated is not null)
            {
                return StatusHandler.ProcessHttpStatusCode(tokenGenrated,"TokenGenerated Succesfully",200);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(tokenGenrated, "Token is Invalid to process or Token Expired",500);
            }
         
        }

        [HttpPost("EmailExists")]
        // PUT api/<LoginController>/5
        public async Task<Responses> EmailExists([FromBody] GetEmail register)
        {
            var isEmailExists = await _authenticationService.EmailExsistsCheck(register.Email);
            if (isEmailExists)
            {
                return StatusHandler.ProcessHttpStatusCode(isEmailExists, "Unique Mail", 200);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(isEmailExists, "User Already Exists", 400);
            }

        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
