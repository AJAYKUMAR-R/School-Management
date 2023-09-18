using BusinessLayer.RespositoryLayer;
using CRUD.RespositoryLayer;
using CRUD.Utils;
using DatabaseLayer.DatabaseLogic.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{page}/{pageSize}")]
        public Responses GetStudents(int page, int pageSize)
        {
            var Student = bl.GetStudentsPerPage(page, pageSize);
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
