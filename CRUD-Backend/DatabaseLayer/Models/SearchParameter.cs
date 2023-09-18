using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class SearchParameter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? DropdownColumn { get; set; }
        public string? DropdownColumnValue { get; set; }
        public string? SortDirection { get; set; }
        public string? SortColumn { get; set; }
    }
}
