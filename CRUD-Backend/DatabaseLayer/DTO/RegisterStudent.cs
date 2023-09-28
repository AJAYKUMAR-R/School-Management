using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DTO
{
    public enum Roles
    {
        Admin,
        User
    }
    public class Register
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student Name is Required")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is not in Correct Format")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+|~=`{}\[\]:;""'<>,.?/])(?!.*\s).{8,}$", ErrorMessage = "Password is not in Correct Format")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Pincode is Required")]
        public long? Pincode { get; set; }
        [Required(ErrorMessage = "Country is Required")]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public string? Roles { get; set; }
    }
}
