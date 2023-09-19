using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public  class PaginationResult
    {
        public IEnumerable<Student>? Result { get; set; }

        public int TotalCount { get; set; }
    }
}
