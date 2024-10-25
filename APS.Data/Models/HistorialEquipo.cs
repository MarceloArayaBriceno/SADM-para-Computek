using System;
using System.Collections.Generic;

namespace APS.Data.Models;

public partial class HistorialEquipo
{
    public int HistorialId { get; set; }

    public int EquipoId { get; set; }

    public string DescripcionCambio { get; set; } = null!;

    public DateTime FechaCambio { get; set; }
}
