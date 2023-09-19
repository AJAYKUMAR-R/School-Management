using BusinessLayer.RespositoryLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public  PaginationResult GetStudentsPerPage(SearchParameter parameter)
        {
            parameter.SortDirection = parameter.SortDirection is null ? "" : parameter.DropdownColumn.ToUpper();
            parameter.DropdownColumnValue = parameter.DropdownColumnValue is null ? "" : parameter.DropdownColumnValue.ToUpper();
            parameter.DropdownColumn = parameter.DropdownColumn is null ? "" : parameter.DropdownColumn.ToUpper();
            var students =  dlPagination.GetStudentsPerPageFromDb(parameter);
            int count = dlPagination.GetTotalCountFromDb(parameter);
            PaginationResult paginationResult= new PaginationResult();  
            if(students.Count() > 0)
            {
                paginationResult.Result = students;
                paginationResult.TotalCount = count;
            }
            else
            {
                paginationResult.Result = null;
                paginationResult.TotalCount = count;
            }

            return paginationResult;
        }

        
    }
}
