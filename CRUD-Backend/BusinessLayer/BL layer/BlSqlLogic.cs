using DatabaseLayer.DatabaseLogic.Models;
using CRUD.RespositoryLayer;
using DatabaseLayer.DatabseAbstraction;
using DatabaseLayer.DatabaseLogic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DatabaseLayer.Models;

namespace CRUD.BL_layer
{
    public class BlSqlLogic : IBlSqlLogic
    {
        private readonly IDLSlqLogic _logic;

        public BlSqlLogic(IDLSlqLogic logic)
        {
            _logic = logic;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _logic.GetStudentsFromDb();
        }

        public async Task<Student> GetStudents(int id)
        {
            var Student = await _logic.GetStudentsFromDb(id);
            if(Student is not null) {
                return Student;  
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateStudent(Student student)
        {
            var flag = await _logic.CreateStudentFromDb(student);
            if (flag) {
                return true;
            }else{
                return false;
            }
        }

        public async Task<Student> UpdateStudent(int id, Student student)
        {
                var existstudent = await _logic.UpdateStudentFromDb(id,student);
                if (existstudent is not null)
                {
                    return existstudent;
                }
                else
                {
                    return null;
                } 
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var flag = await _logic.DeleteStudentFromDb(id);

            if (flag)
            {
                return true;
            }
            else
            {
               return false;
            }
        }

        
    }
}
