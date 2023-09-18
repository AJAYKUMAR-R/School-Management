using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;

namespace CRUD.RespositoryLayer
{
    public interface IBlSqlLogic
    {
        public  Task<IEnumerable<Student>> GetStudents();
        public  Task<Student> GetStudents(int id);
        public Task<bool> CreateStudent(Student student);
        public  Task<Student> UpdateStudent(int id, Student student);
        public Task<bool> DeleteStudent(int id);

    }
}
