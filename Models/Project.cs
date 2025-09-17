using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Project
{
    public int Pnum { get; set; }

    public string? Pname { get; set; }

    public string? Location { get; set; }

    public string? City { get; set; }

    public int? Dnum { get; set; }

    public virtual Department? DnumNavigation { get; set; }

    public virtual ICollection<EmpManageDep> EmpManageDeps { get; set; } = new List<EmpManageDep>();
}
