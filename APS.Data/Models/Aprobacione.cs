using System;
using System.Collections.Generic;

namespace APS.Data.Models;

public partial class Aprobacione
{
    public int AprobacionId { get; set; }

    public int EquipoId { get; set; }

    public string Criterio { get; set; } = null!;

    public bool Cumple { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;
}
