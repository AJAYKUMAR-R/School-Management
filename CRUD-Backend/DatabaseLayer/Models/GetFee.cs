using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    [Keyless]
    public class GetFee
    {
      
      public int FeeId { get; set; }

        public decimal? ExamFee { get; set; }

        public decimal? TutionFee { get; set; }

        public decimal? BusFee { get; set; }

        public decimal? TotalFee { get; set; }

        public bool? IsPaid { get; set; }
    }
}
