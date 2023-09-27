using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class StudentProfile
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Email { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public long? Pincode { get; set; }

    public string? Country { get; set; }

    public string? Roles { get; set; }
}
