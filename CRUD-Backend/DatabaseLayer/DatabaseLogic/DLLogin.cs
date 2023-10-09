using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabaseLogic
{
    public class DLLogin : IDLLogin
    {
        private readonly CrudContext _dbcontext;

        public DLLogin(CrudContext _dbcontext)
        {
            this._dbcontext = _dbcontext;
        }

        public async Task<bool> RegisterUser(UserProfile student)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter ("@studentName",student.StudentName),
                new SqlParameter ("@email",student.Email),
                new SqlParameter ("@passwordSalt",student.PasswordSalt),
                new SqlParameter ("@passwordHash",student.PasswordHash),
                new SqlParameter ("@Pincode",student.Pincode),
                new SqlParameter ("@Country",student.Country),
                new SqlParameter ("@Roles",student.Roles)
            };

            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync("EXEC [DBO].[ADDRECORDINTOSTUDENTPROFILE] @studentName,@email,@passwordSalt,@passwordHash,@Pincode,@Country,@Roles", sqlParameter);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<UserProfile> GetUser(User student)
        {
            //seting the values
            SqlParameter[] sqlParameter = new SqlParameter[]{
               new SqlParameter ("@email",student.Email)
            };

            try
            {
                var students = _dbcontext.UserProfiles.FromSqlRaw("EXEC [DBO].[SEARCHUSER] @email"
                    , sqlParameter)
                    .ToList().FirstOrDefault();
                return students;

            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateRefreshToken(string refreshToken,DateTime? refreshExpireTime,string email)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter ("@refershToken",System.Data.SqlDbType.VarChar,255) { Value = refreshToken},
                new SqlParameter ("@refreshTokenExpireDate",System.Data.SqlDbType.DateTime) { Value = refreshExpireTime},
                new SqlParameter ("@email",System.Data.SqlDbType.VarChar,255){ Value = email }
            };

            try
            {
                var count = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC [DBO].[CREATEREFRESHTOKEN] @refershToken,@refreshTokenExpireDate,@email", sqlParameter);
                if(count > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> SearchRefreshToken(string refreshToken)
        {

            try
            {
                var isRefresh =  await _dbcontext.UserProfiles.AnyAsync((user) => user.RefreshToken.Equals(refreshToken));
                if(isRefresh)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }

        public Task<bool> EmailExists(string email)
        {
            SqlParameter sqlParameter = new SqlParameter("@email", System.Data.SqlDbType.VarChar, 255) { Value = email.Trim() };
            List<UserProfile> student = null;
            try
            {
                student = _dbcontext.UserProfiles.FromSqlRaw("EXEC EMAILEXISTS @email", sqlParameter).ToList();
              
            }
            catch(Exception ex)
            {
                return Task.FromResult(false);
            }
            if(student is not null)
            {
                if (student?.Count > 0)
                    return Task.FromResult(false);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }


    }
}
