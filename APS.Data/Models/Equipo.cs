using System;
using System.Collections.Generic;

namespace APS.Data.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string NombreCliente { get; set; } = null!;

    public string MotivoIngreso { get; set; } = null!;

    public bool GarantiaConLocal { get; set; }

    public string? ContraseñaEquipo { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaIngreso { get; set; }

    public int? UsuarioId { get; set; }
}
