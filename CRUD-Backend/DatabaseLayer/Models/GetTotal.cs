using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    [Keyless]
    public class GetTotal
    {
        public int TotalCount { get; set; }
    }
}
