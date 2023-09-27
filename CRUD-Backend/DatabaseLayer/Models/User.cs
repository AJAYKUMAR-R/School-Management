using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public byte[]? PasswordHash { get; set; }    
        public byte[]? PasswordSalt { get; set;}
    }
}
