using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabaseLogic
{
    public class DLPagination : IDLPagination
    {
        private readonly CrudContext _dbcontext;
        public DLPagination(CrudContext _dbcontext)
        {
            this._dbcontext = _dbcontext;
        }

        public  IEnumerable<Student> GetStudentsPerPageFromDb(int page, int pageSize)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]{
               new SqlParameter ("@pagenumber",page),
               new SqlParameter ("@pageSize",pageSize)
            };

            try
            {
                var students = _dbcontext.Students.FromSqlRaw("EXEC [DBO].[Getstudents] @pagenumber,@pageSize", sqlParameter)
                    .AsEnumerable();
                return students;

            }
            catch
            {
                return null;
            }
        }

        public async Task<int> GetTotalCountFromDb()
        {
            return await _dbcontext.Students.CountAsync();
        }
    }
}
