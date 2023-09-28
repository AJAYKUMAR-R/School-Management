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
using Microsoft.Data.SqlClient;
using DatabaseLayer.DatabaseLogic;
using DatabaseLayer.DatabaseAbstraction;
using BusinessLayer.RespositoryLayer;

namespace BusinessLayer.BL_layer
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IDLLogin _iDLLogin;
        //private readonly User user1 = new User();
        public AuthenticationService(IConfiguration _configuration, IDLLogin iDLLogin)
        {
            this._configuration = _configuration;
            this._iDLLogin= iDLLogin;
        }

        public async Task<string> SignIn(User user)
        {
            StudentProfile st = await _iDLLogin.GetUser(user);
            if (st != null)
            {
                var flag = this.VerifyPasswordHash(user.Password,st.PasswordHash,st.PasswordSalt);
                if (flag)
                {
                    return this.CreateJwTtoken(st);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            } 
        }


        public async Task<bool> SignUp(Register user)
        {
            if (user is not null)
            {
                StudentProfile studentProfile = new StudentProfile();
                studentProfile.Country = user.Country;
                studentProfile.Roles = user.Roles;
                studentProfile.Email = user.Email;
                studentProfile.Pincode = user.Pincode;
                studentProfile.StudentName = user.StudentName;
                //Hasing the password
                this.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                studentProfile.PasswordHash = passwordHash;
                studentProfile.PasswordSalt = passwordSalt;
                var flag = await _iDLLogin.RegisterUser(studentProfile);
                if (flag)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }

        public string CreateJwTtoken(StudentProfile user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.StudentName),
                new Claim("Role", user.Roles)
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


        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            ///intialize the Object with Randomly Generated Key
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }




        //Geting the Hash and Salt from DB then produce the pasword with passwordSalt from the DB
        //if it produce the same hash using the password salt from the DB the password is correct 
        //other wise it will be false
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //param passwordSalt
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                //password param 
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //compares hash from generated hash
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
