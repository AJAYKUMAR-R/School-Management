﻿using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabaseAbstraction
{
    public interface IDLPagination
    {
        public IEnumerable<Student> GetStudentsPerPageFromDb(SearchParameter parameter);
        public int GetTotalCountFromDb(SearchParameter parameter);
    }
}
