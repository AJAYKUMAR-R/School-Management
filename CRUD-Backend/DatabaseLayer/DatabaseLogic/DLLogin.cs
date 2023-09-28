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

        public async Task<bool> RegisterUser(StudentProfile student)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter  ("@studentName",student.StudentName),
                new SqlParameter  ("@email",student.Email),
                new SqlParameter  ("@passwordSalt",student.PasswordSalt),
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

        public async Task<StudentProfile> GetUser(User student)
        {
            //seting the values
            SqlParameter[] sqlParameter = new SqlParameter[]{
               new SqlParameter ("@email",student.Email)
            };

            try
            {
                var students = _dbcontext.StudentProfiles.FromSqlRaw("EXEC [DBO].[SEARCHUSER] @email"
                    , sqlParameter)
                    .ToList().FirstOrDefault();
                return students;

            }
            catch
            {
                return null;
            }
        }



    }
}
