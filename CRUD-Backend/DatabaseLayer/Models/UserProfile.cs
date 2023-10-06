using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class UserProfile
{
    public int StudentId { get; set; }

    public Guid? StudentGuid { get; set; }

    public string? StudentName { get; set; }

    public string Email { get; set; } = null!;

    public byte[]? PasswordSalt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public long? Pincode { get; set; }

    public string? Country { get; set; }

    public string? Roles { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshExpireTime { get; set; }

    public bool? Isdeleted { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }
}
