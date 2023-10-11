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
        public StudentController(IBLFee _bl)
        {
            this._bl = _bl;
        }

        // GET api/<StudentController>/5
        [HttpGet("{studentId}")]
        public async Task<Responses> FeeStatus(int studentId)
        {
            if(studentId != 0)
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

        
       
    }
}
