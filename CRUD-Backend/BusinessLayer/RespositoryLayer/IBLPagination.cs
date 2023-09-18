﻿using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RespositoryLayer
{
    public interface IBLPagination
    {
        public IEnumerable<Student> GetStudentsPerPage(SearchParameter parameter);
        public Task<int> GetTotalCount();
    }
}
