using BusinessLayer.RespositoryLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL_layer
{
    public class BLPagination : IBLPagination
    {
        private readonly IDLPagination? dlPagination;
        public BLPagination(IDLPagination dlPagination)
        {
            this.dlPagination = dlPagination;
        }

        public  IEnumerable<Student> GetStudentsPerPage(SearchParameter parameter)
        {
            var students =  dlPagination.GetStudentsPerPageFromDb(parameter);
            return students;
        }

        public async Task<int> GetTotalCount()
        {
            return await dlPagination.GetTotalCountFromDb();
        }
    }
}
