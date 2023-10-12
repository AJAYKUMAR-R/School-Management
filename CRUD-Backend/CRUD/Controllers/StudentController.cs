using BusinessLayer.RespositoryLayer;
using CRUD.RespositoryLayer;
using CRUD.Utils;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.Controllers
{
    [Route("CRUD/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IBLFee _bl;
        private readonly IAuthenticationService _authorize;
        public StudentController(IBLFee _bl,IAuthenticationService _authorize)
        {
            this._bl = _bl;
            this._authorize = _authorize;   
        }

        // GET api/<StudentController>/5
        [HttpGet("{email}")]
        public async Task<Responses> FeeStatus(string email)
        {
            var StudentProfile = await _bl.GetUserProfile(email);
            int studentId = StudentProfile is not null? StudentProfile.StudentId : 0;
            if (studentId != 0)
            {
                var studentFee = await _bl.CheckFeeStatus(studentId);
                if(studentFee is not null)
                {
                    return StatusHandler.ProcessHttpStatusCode(studentFee, "Data Fetched succesfully", 200);
                }
                else
                {
                    return StatusHandler.ProcessHttpStatusCode(null, "Record not Found", 404);
                }
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(null, "Invalid Data", 400);
            }
        }

        [HttpGet("GetProfile/{email}")]
        public async Task<Responses> GetProfile(string email)
        {
            var StudentProfile = await _bl.GetUserProfile(email);
            if (StudentProfile is not null)
            {
                Profile profile = new Profile();
                profile.StudentName = StudentProfile.StudentName;
                profile.Email = StudentProfile.Email;
                profile.StudentGuid = StudentProfile.StudentGuid;
                profile.Roles = StudentProfile.Roles;
                profile.Country = StudentProfile.Country;
                profile.Pincode = StudentProfile.Pincode;
                return StatusHandler.ProcessHttpStatusCode(profile, "Data Fetched succesfully", 200);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(null, "Record not Found", 404);
            }
        }



    }
}
