using BusinessLayer.RespositoryLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL_layer
{
    public class BlFee:IBLFee
    {
        private readonly IDLFeeLogic? _feeLogic;
        public BlFee(IDLFeeLogic _feeLogic)
        {
            this._feeLogic = _feeLogic;
        }


        public async Task<GetFee> CheckFeeStatus(int studentId)
        {
            var studentFee = await _feeLogic.FeeStatus(studentId);
            return studentFee is not null ? studentFee : null;
        }
    }
}
