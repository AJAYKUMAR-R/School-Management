using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabaseAbstraction
{
    public interface IDLFeeLogic
    {
        Task<GetFee> FeeStatus(int studentId);
    }
}
