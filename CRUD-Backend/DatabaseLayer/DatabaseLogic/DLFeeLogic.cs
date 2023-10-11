using DatabaseLayer.DatabaseAbstraction;
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
    public class DLFeeLogic: IDLFeeLogic
    {
        private readonly CrudContext _dbcontext;

        public DLFeeLogic(CrudContext _dbcontext)
        {
            this._dbcontext = _dbcontext;
        }
        public async Task<GetFee> FeeStatus(int studentId)
        {
            SqlParameter sqlParameter = new SqlParameter("@studentId", DbType.Int64) { Value = studentId};

            try
            {
                var students = _dbcontext.GetFee.FromSqlRaw("EXEC [DBO].[CHECKFEESTATUS] @studentId "
                    , sqlParameter)
                    .ToList().FirstOrDefault();
                return await Task.FromResult(students);
            }catch(SqlException ex)
            {
                return null;
            }

        }

       
    }
}
