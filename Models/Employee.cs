using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Employee
{
    public int Ssn { get; set; }

    public DateOnly Birthdate { get; set; }

    public string First { get; set; } = null!;

    public string Last { get; set; } = null!;

    public string? Gender { get; set; }

    public int? Managerid { get; set; }

    public int? Dnum { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public virtual Department? DnumNavigation { get; set; }

    public virtual ICollection<EmpManageDep> EmpManageDeps { get; set; } = new List<EmpManageDep>();

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Employee? Manager { get; set; }
}
