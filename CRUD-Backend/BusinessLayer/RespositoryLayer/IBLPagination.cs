using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RespositoryLayer
{
    public interface IBLPagination
    {
        public IEnumerable<Student> GetStudentsPerPage(int page, int Pagesize);
        public Task<int> GetTotalCount();
    }
}
