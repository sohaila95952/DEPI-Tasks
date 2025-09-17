using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Department
{
    public int Dnum { get; set; }

    public string Dname { get; set; } = null!;

    public DateOnly? Hiredate { get; set; }

    public int? Ssn { get; set; }

    public virtual ICollection<DepartmentLocation> DepartmentLocations { get; set; } = new List<DepartmentLocation>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Employee? SsnNavigation { get; set; }
}
