using DatabaseLayer.DatabseAbstraction;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DatabaseLayer.DatabaseLogic
{
    public class DLSlqLogic : IDLSlqLogic
    {
        private readonly CrudContext _dbcontext;
        public DLSlqLogic(CrudContext _dbcontext)
        {
            this._dbcontext = _dbcontext;
        }

        public async Task<IEnumerable<Student>> GetStudentsFromDb()
        {
            return await _dbcontext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentsFromDb(int id)
        {
            var Student = await _dbcontext.Students.FirstOrDefaultAsync(data => data.StudentId == id);
            return Student;
        }

        public async Task<bool> CreateStudentFromDb(Student student)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]{
               new SqlParameter ("@NAME",student.StudentName),
               new SqlParameter ("@AGE",student.Age),
               new SqlParameter ("@GRADE",student.Grade),
            };

            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync("EXEC [DBO].[ADDRECORD] @NAME,@AGE,@GRADE", sqlParameter);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Student> UpdateStudentFromDb(int id, Student student)
        {
            var exsistingStudent = await _dbcontext.Students.Where((obj)=> obj.StudentId == id).FirstOrDefaultAsync();

            if (exsistingStudent is not null)
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
            new SqlParameter ("@ID",id),
            new SqlParameter ("@NAME",student.StudentName),
            new SqlParameter ("@AGE",student.Age),
            new SqlParameter ("@GRADE",student.Grade),
            };

                try
                {
                    int updatedcount = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC [DBO].[UPDATESTUDENTS] @ID,@NAME,@AGE,@GRADE", sqlParameter);
                    if (updatedcount == 1)
                    {
                        student.StudentId = id;
                        return student;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
         
        }

        public async Task<bool> DeleteStudentFromDb(int id)
        {
            var exsistingStudent = await  _dbcontext.Students.Where((obj) => obj.StudentId == id).FirstOrDefaultAsync();

            if(exsistingStudent is not null)
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                   new SqlParameter ("@ID",id)
                };

                try
                {
                    int updatedcount = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC [DBO].[DELETESTUDENTS] @ID", sqlParameter);
                    if (updatedcount == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
           

           
        }
    }
}
