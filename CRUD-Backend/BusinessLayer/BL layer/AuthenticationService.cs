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
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

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

        public async Task<UserTokens> SignIn(User user)
        {
            UserProfile st = await _iDLLogin.GetUser(user);
            if (st != null)
            {
                var flag = this.VerifyPasswordHash(user.Password,st.PasswordHash,st.PasswordSalt);
                if (flag)
                {
                    string refreshToken = await this.CreateRefreshToken();
                    string jwtToken = this.CreateJwTtoken(st);
                    var userDetails = await _iDLLogin.GetUser(user);
                    if (userDetails.RefreshExpireTime < DateTime.Now || userDetails.RefreshExpireTime is null)
                    {
                        var isAdded = await _iDLLogin.CreateRefreshToken(refreshToken,user.Email,DateTime.Now.AddMinutes(40));
                        if (isAdded)
                            return new UserTokens()
                            {
                                RefreshTokens = refreshToken,
                                JwtTokens = jwtToken
                            };
                        return null;
                    }
                    else
                    {
                        var isAdded = await _iDLLogin.CreateRefreshToken(refreshToken, user.Email);
                        if (isAdded)
                            return new UserTokens()
                            {
                                RefreshTokens = refreshToken,
                                JwtTokens = jwtToken
                            };
                        return null;
                    }
                   
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
                UserProfile studentProfile = new UserProfile();
                studentProfile.Country = user.Country;
                studentProfile.Roles = user.Roles;
                studentProfile.UserMail = user.Email;
                studentProfile.Pincode = user.Pincode;
                studentProfile.UserName = user.StudentName;
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

        public string CreateJwTtoken(UserProfile user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Name", user.UserName),
                new Claim("Role", user.Roles),
                new Claim("Email", user.UserMail)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("TokenName").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: creds
                );

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

        public async Task<string> CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            var tokenInUser = await _iDLLogin.SearchRefreshToken(refreshToken);
            if (tokenInUser)
            {
                return await CreateRefreshToken();
            }
            return refreshToken;
        }


        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            //generating Symentric Key
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("TokenName").Value));

            //Seting up the Token Validator
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = true,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            //Return the Claims principle
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            //convert Security Token to Orginal Token
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
               //|| !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase)
                throw new SecurityTokenException("This is Invalid Token");
            return principal;

        }

        public async Task<UserTokens> UpdateRefreshTokens(string RefreshToken, string oldJwttoken)
        {
            User userProfile = new User();
            var principal = GetPrincipleFromExpiredToken(oldJwttoken);
            var userClaims = principal.Claims.FirstOrDefault(c => c.Type == "Email");
            userProfile.Email = userClaims.Value;
            //hiting the server 
            var user = await _iDLLogin.GetUser(userProfile);
            //comparing with DB data
            if (user is not null && user.RefreshToken.Equals(user.RefreshToken) && (user.RefreshExpireTime > DateTime.Now))
            {
                var newJwtToken = this.CreateJwTtoken(user);
                var newRefreshToken = await CreateRefreshToken();
                var isAdded = await _iDLLogin.CreateRefreshToken(newRefreshToken,userProfile.Email);
                return new UserTokens()
                {
                    JwtTokens = newJwtToken,
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> EmailExsistsCheck(string email)
        {
                if (!email.IsNullOrEmpty())
                {
                    var ismail = await _iDLLogin.EmailExists(email);
                    return ismail ? true : false;
                }
                else
                {
                    return true;
                }
        }

        public async Task<string> getRefreshToken(string jwt)
        {
            if (!jwt.IsNullOrEmpty())
            {
                var userProfile = new User();
                var principal = GetPrincipleFromExpiredToken(jwt);
                var userClaims = principal.Claims.FirstOrDefault(c => c.Type == "Email");
                userProfile.Email = userClaims.Value;
                var user = await _iDLLogin.GetUser(userProfile);
                return user.RefreshToken is not null ? user.RefreshToken : null;
            }
            else
            {
                return null;
            }
        }

        


    }
}
