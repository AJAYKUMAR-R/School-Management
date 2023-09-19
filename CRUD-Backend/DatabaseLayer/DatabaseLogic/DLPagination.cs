using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
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
               new SqlParameter ("@SortColumn",parameter.SortColumn?.ToUpper())
            };

            try
            {
                var students = _dbcontext.Students.FromSqlRaw("EXEC [DBO].[RECORDSPERPAGE] @pagenumber,@pageSize,@DropdownColumn,@DropdownColumnValue,@SortDirection,@SortColumn"
                    , sqlParameter)
                    .ToList();
                return students;

            }
            catch
            {
                return null;
            }
        }

        public  int GetTotalCountFromDb(SearchParameter parameter)
        {
            var outputParameter = new SqlParameter()
            {
                ParameterName = "OutputParameter",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };

            int count = 0;

            try
            {
                var results = _dbcontext.GetTotal
                            .FromSqlInterpolated($@"
                                EXEC [GETTOTAL]
                                    @DropdownColumn = {parameter.DropdownColumn},
                                    @DropdownColumnValue = {parameter.DropdownColumnValue},
                                    @TOTALCOUNT = {outputParameter} OUTPUT")
                            .ToList();
                count = (int)outputParameter.Value;
                return count;

            }
            catch
            {
                return count;
            }
        }
    }
}
