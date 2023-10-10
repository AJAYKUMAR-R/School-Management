using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabaseAbstraction
{
    public interface IDLLogin
    {
        public Task<bool> RegisterUser(UserProfile student);
        public Task<UserProfile> GetUser(User student);
        Task<bool> SearchRefreshToken(string refreshToken);
        Task<bool> CreateRefreshToken(string refreshToken, string email, DateTime? refreshExpireTime = null);
        Task<bool> EmailExists( string email);
    }
}
