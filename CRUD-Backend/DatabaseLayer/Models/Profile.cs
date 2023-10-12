using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class Profile
    {
        public Guid? StudentGuid { get; set; }
        public string? StudentName { get; set; }
        public string Email { get; set; } = null!;
        public long? Pincode { get; set; }
        public string? Country { get; set; }
        public string? Roles { get; set; }

    }
}
