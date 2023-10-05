using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DTO
{
    public class User
    {
        //[Required(ErrorMessage = "UserName is Required")]
        public string? Email { get; set; }
        //[Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
    }
}
