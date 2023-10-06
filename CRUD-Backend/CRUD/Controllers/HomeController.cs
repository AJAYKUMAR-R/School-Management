using DatabaseLayer.DatabaseLogic.Models;
using CRUD.RespositoryLayer;
using CRUD.Utils;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Authorization;
using CRUD.Filters;

//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.Controllers
{
    [Route("CRUD/[controller]")]
    [ApiController]
    [Authorize]
    //[CustomAuthorize("User")]
    public class HomeController : ControllerBase
    {
        private readonly IBlSqlLogic bl;
        public HomeController(IBlSqlLogic bl)
        {
            this.bl = bl;
        }

        // GET: api/<HomeController>
        [HttpGet]
        public async Task<Responses> Get()
        {
            var Student = await bl.GetStudents();

            if (Student is not null)
            {
                return StatusHandler.ProcessHttpStatusCode(Student, "Data Succesfully Fetched from the Db", 200);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(Student, null,500);
            }

        }


        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public async Task<Responses> Get(int id)
        {
            Student data = await bl.GetStudents(id);
            if (data is not null)
            {
                return StatusHandler.ProcessHttpStatusCode(data, "Data is Fetched up from the Db",200);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(data, null,500);
            }

        }

        // POST api/<HomeController>
        [HttpPost]
        public async Task<Responses> Post([FromBody] Student value)
        {

            bool flag = await bl.CreateStudent(value);
            if (flag)
            {
                return StatusHandler.ProcessHttpStatusCode(flag, "Record Created SuccessFully",204);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(flag, "Can't proceess the Data",500);
            }


        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public async Task<Responses> Put(int id, [FromBody] Student value)
        {
            Student student = await bl.UpdateStudent(id, value);
            if (student is not null)
            {
                return StatusHandler.ProcessHttpStatusCode(student, "Updated Succesfully",204);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(student, null,500);
            }
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public async Task<Responses> Delete(int id)
        {
            var flag = await bl.DeleteStudent(id);
            if (flag)
            {
                return StatusHandler.ProcessHttpStatusCode(flag, "SuccesFully Deleted",204);
            }
            else
            {
                return StatusHandler.ProcessHttpStatusCode(flag, null,500);
            }


        }
    }
}
