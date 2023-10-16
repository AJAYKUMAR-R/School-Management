using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class UserProfile
{
    public int UserId { get; set; }

    public Guid? UserGuid { get; set; }

    public string? UserName { get; set; }

    public string UserMail { get; set; } = null!;

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
