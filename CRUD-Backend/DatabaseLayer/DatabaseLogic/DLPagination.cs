using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        public  IEnumerable<Student> GetStudentsPerPageFromDb(SearchParameter parameter)
        {
            //seting the values


            SqlParameter[] sqlParameter = new SqlParameter[]{
               new SqlParameter ("@pagenumber",parameter.Page),
               new SqlParameter ("@pageSize",parameter.PageSize),
               new SqlParameter ("@DropdownColumn",parameter.DropdownColumn?.ToUpper()),
               new SqlParameter ("@DropdownColumnValue",parameter.DropdownColumnValue?.ToUpper()),
               new SqlParameter ("@SortDirection",parameter.SortDirection?.ToUpper()),
               new SqlParameter ("@SortColumn",parameter.SortDirection?.ToUpper())
            };

            var outputParameter = new SqlParameter
            {
                ParameterName = "@OutputParameter",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            try
            {
                var students = _dbcontext.Students.FromSqlRaw("EXEC [DBO].[RECORDSPERPAGE] @pagenumber,@pageSize,@DropdownColumn,@DropdownColumnValue,@SortDirection,@SortColumn"
                    , sqlParameter)
                    .ToList();
                var tcount = (int)outputParameter.Value;
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
