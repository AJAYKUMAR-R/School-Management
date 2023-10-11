using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class StudentFee
{
    public int FeeId { get; set; }

    public int StudentId { get; set; }

    public decimal? ExamFee { get; set; }

    public decimal? TutionFee { get; set; }

    public decimal? BusFee { get; set; }

    public decimal? TotalFee { get; set; }

    public bool? IsPaid { get; set; }

    public DateTime? PaidDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual UserProfile Student { get; set; } = null!;
}
