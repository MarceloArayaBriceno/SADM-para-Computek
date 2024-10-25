using System;
using System.Collections.Generic;

namespace APS.Data.Models;

public partial class Aprobacione
{
    public string Criterio { get; set; } = null!;

    public bool Cumple { get; set; }

    public int AprobacionId { get; set; }
}
