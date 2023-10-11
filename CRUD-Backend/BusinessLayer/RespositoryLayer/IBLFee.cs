using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RespositoryLayer
{
    public interface IBLFee
    {
        public Task<GetFee> CheckFeeStatus(int studentId);
    }
}
