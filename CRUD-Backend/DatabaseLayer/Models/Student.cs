using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public int? Age { get; set; }

    public int? Grade { get; set; }
}
