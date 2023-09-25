using Azure;
using BusinessLayer.BL_layer;
using CRUD.Utils;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

         UserDto _user=new UserDto();

        public AuthenticationService AuthenticationService { get; }

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
        [HttpPost]
        public Responses Post([FromBody] UserDto Dto)
        {
            if(Dto is not null)
            {
                User user = new User();
                user.Username = Dto.Username;
                user.Password = Dto.Password;
                var flag = _authenticationService.SignIn(user);
                if (flag is not null)
                {
                    return StatusHandler.ProcessHttpStatusCode(flag, "Login SuccesFull");
                }
                else
                {
                    return StatusHandler.ProcessHttpStatusCode(null,null);
                }
                //_user.Username= Dto.Username;
                //_user.Password= Dto.Password;
                //CreatePasswordHash(_user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                //User user = new User();
                //user.PasswordHash = passwordHash;
                //user.PasswordSalt = passwordSalt;
                //string token = CreateToken(user);
                //return token;
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(null, null);
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("TokenName").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            ///intialize the Object with Randomly Generated Key
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }



        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public class UserDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        //public class User
        //{
        //    public string Username { get; set; } = string.Empty;
        //    public byte[] PasswordHash { get; set; }
        //    public byte[] PasswordSalt { get; set; }
        //    public string RefreshToken { get; set; } = string.Empty;
        //    public DateTime TokenCreated { get; set; }
        //    public DateTime TokenExpires { get; set; }
        //}

    }
}
