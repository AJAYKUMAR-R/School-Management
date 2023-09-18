using  DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabseAbstraction
{
    public interface IDLSlqLogic
    {
       
        public  Task<IEnumerable<Student>> GetStudentsFromDb();
        public  Task<Student> GetStudentsFromDb(int id);
        public  Task<bool> CreateStudentFromDb(Student student);
        public  Task<Student> UpdateStudentFromDb(int id, Student student);
        public Task<bool> DeleteStudentFromDb(int id);
       
    }
}
