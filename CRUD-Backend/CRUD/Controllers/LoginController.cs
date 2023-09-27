﻿using Azure;
using BusinessLayer.BL_layer;
using CRUD.Utils;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;
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
        private readonly IConfiguration _configuration;
        private readonly AuthenticationService _authenticationService;

        public LoginController(IConfiguration _configuration, AuthenticationService authenticationService)
        {
           this._configuration = _configuration;
            _authenticationService = authenticationService;
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

        // POST api/<LoginController>
        //[HttpPost]
        //public Responses SignIn([FromBody] RegisterStudent register)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = _authenticationService.SignUp(register);
        //        if(result)
        //        {
        //            return StatusHandler.ProcessHttpStatusCode(register, "Registered Succes Fully");
        //        }
        //        else
        //        {
        //            return StatusHandler.ProcessHttpStatusCode(null, "Internall Server Error");
        //        }
        //    }
        //    else
        //    {
        //        List<String> errors = new List<String>();
        //        foreach (var key in ModelState.Keys)
        //        {
        //            var entry = ModelState[key];
        //            foreach (var error in entry.Errors)
        //            {
        //                errors.Add(error.ErrorMessage);
        //            }
        //        }
        //        return StatusHandler.ProcessHttpStatusCode(errors,"Validation Fails");
        //    }
        //}

        [HttpPost("SignUp")]
        public Responses SignUp([FromBody] RegisterStudent register)
        {
            if (ModelState.IsValid)
            {
                var result = _authenticationService.SignUp(register);
                if (result)
                {
                    return StatusHandler.ProcessHttpStatusCode(register, "Registered Succes Fully");
                }
                else
                {
                    return StatusHandler.ProcessHttpStatusCode(null, "Internall Server Error");
                }
            }
            else
            {
                List<String> errors = new List<String>();
                foreach (var key in ModelState.Keys)
                {
                    var entry = ModelState[key];
                    foreach (var error in entry.Errors)
                    {
                        
                        errors.Add(error.ErrorMessage);
                    }
                }
                return StatusHandler.ProcessHttpStatusCode(errors, "Validation Fails");
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