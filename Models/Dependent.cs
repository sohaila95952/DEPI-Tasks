using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Dependent
{
    public string Depname { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public int Essn { get; set; }

    public virtual Employee EssnNavigation { get; set; } = null!;
}
