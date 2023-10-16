using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class StudentProfile
{
    public int StudentProfileId { get; set; }

    public int StudentId { get; set; }

    public string Gender { get; set; } = null!;

    public string? MotherName { get; set; }

    public string? FatherName { get; set; }

    public int Grade { get; set; }

    public long Phone { get; set; }

    public string? FatherOccupation { get; set; }

    public string? MotherOccupation { get; set; }

    public decimal? AnnualIncome { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual UserProfile Student { get; set; } = null!;
}
