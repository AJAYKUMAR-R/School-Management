using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RespositoryLayer
{
    public interface IAuthenticationService
    {

        Task<string> SignIn(User user);
        Task<bool> SignUp(Register user);
        string CreateJwTtoken(StudentProfile user);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    }
}
