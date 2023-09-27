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
using System.Security.Cryptography;
using DatabaseLayer.DTO;

namespace BusinessLayer.BL_layer
{
    public class AuthenticationService
    {
        private readonly IConfiguration _configuration;
        //private readonly User user1 = new User();
        public AuthenticationService(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        //public string  SignIn(User user)
        //{
        //    //if(user.Username == "Ajay" && user.Password == "123")
        //    //{
        //    //    var boolean = VerifyPasswordHash(user.Password, user1.PasswordHash, user1.PasswordSalt);
        //    //    if(boolean)
        //    //    {
        //    //        return CreateJwTtoken(user);
        //    //    }
        //    //    else
        //    //    {
        //    //        return null;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return null;
        //    //}
        //}


        public bool SignUp(RegisterStudent user)
        {
            return true;
        }


        public string CreateJwTtoken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.Username),
                new Claim("Role", "Admin")
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
    }
}
