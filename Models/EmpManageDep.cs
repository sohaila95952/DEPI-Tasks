using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class EmpManageDep
{
    public int? WorkingHours { get; set; }

    public int Essn { get; set; }

    public int Pnum { get; set; }

    public virtual Employee EssnNavigation { get; set; } = null!;

    public virtual Project PnumNavigation { get; set; } = null!;
}
