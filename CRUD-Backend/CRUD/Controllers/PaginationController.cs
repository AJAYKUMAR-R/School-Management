using BusinessLayer.RespositoryLayer;
using CRUD.RespositoryLayer;
using CRUD.Utils;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace CRUD.Controllers
{
    [Route("CRUD/[controller]")]
    [ApiController]
    public class PaginationController : ControllerBase
    {
        private readonly IBLPagination bl;
        public PaginationController(IBLPagination bl)
        {
            this.bl = bl;
        }

        [HttpPost]
        public Responses GetStudents([FromBody] SearchParameter value)
        {
            var Student = bl.GetStudentsPerPage(value);
            if (Student is not null)
            {
                return StatusHandler.ProcessHttpStatusCode(Student, "Data Succesfully Fetched from the Db");
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(Student, null);
            }

        }

        [HttpGet]
        public async Task<Responses> GetTotalRecords()
        {
            int count = await bl.GetTotalCount();
            if (count != 0)
            {
                return StatusHandler.ProcessHttpStatusCode(count, "Data Succesfully Fetched from the Db");
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(count, null);
            }
        }
    }
}
