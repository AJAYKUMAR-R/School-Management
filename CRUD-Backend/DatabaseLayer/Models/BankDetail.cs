using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class BankDetail
{
    public int BankDetailsId { get; set; }

    public int StudentId { get; set; }

    public string? AccountNumber { get; set; }

    public string? Branch { get; set; }

    public long? IfcsCode { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual UserProfile Student { get; set; } = null!;
}
