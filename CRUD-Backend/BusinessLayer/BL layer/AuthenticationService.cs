using Microsoft.Extensions.Configuration;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLayer.BL_layer
{
    public class AuthenticationService
    {
        private readonly IConfiguration _configuration;
        public AuthenticationService(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public string  SignIn(User user)
        {
            if(user.Username == "Ajay" && user.Password == "123")
            {
                
                return CreateJwTtoken(user); ;
            }
            else
            {
                return null;
            }
        }


        public string CreateJwTtoken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.Username),
                new Claim("role", "Admin")
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
    }
}
