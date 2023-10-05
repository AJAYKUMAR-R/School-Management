using BusinessLayer.RespositoryLayer;
using CRUD.Filters;
using CRUD.RespositoryLayer;
using CRUD.Utils;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Reflection.Metadata;

namespace CRUD.Controllers
{
    [Route("CRUD/[controller]")]
    [ApiController]
    [CustomAuthorize("Admin")]
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

      
    }
}
