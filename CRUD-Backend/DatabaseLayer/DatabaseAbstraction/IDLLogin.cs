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
        public Task<bool> RegisterUser(StudentProfile student);
        public Task<StudentProfile> GetUser(User student);
    }
}
