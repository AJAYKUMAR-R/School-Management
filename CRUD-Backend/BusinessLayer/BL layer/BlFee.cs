using BusinessLayer.PDF;
using BusinessLayer.RespositoryLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using QuestPDF.Fluent;
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
        private readonly IDLLogin? _auth;
        public BlFee(IDLFeeLogic _feeLogic, IDLLogin? auth)
        {
            this._feeLogic = _feeLogic;
            _auth = auth;
        }


        public async Task<GetFee> CheckFeeStatus(int studentId)
        {
            var studentFee = await _feeLogic.FeeStatus(studentId);
            return studentFee is not null ? studentFee : null;
        }

        public async Task<UserProfile> GetUserProfile(string email)
        {
            User users = new User();
            users.Email = email;
            var user = await _auth.GetUser(users);
            return user is not null ? user : null;
        }

        public async Task<byte[]> GeneratePDF(int id)
        {
            var studentFee = await _feeLogic.FeeStatus(id);
            FeePDF pDF = new FeePDF(studentFee);
            return  pDF.GeneratePdf();
        }
    }
}
