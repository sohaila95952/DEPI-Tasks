using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class DepartmentLocation
{
    public string Location { get; set; } = null!;

    public int Dnum { get; set; }

    public virtual Department DnumNavigation { get; set; } = null!;
}
