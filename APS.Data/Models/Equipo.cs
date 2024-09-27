namespace APS.Data.Models
{
    public partial class Equipo
    {
        public int EquipoId { get; set; }

        public string? Marca { get; set; }  // Puede ser NULL

        public string? Modelo { get; set; }  // Puede ser NULL

        public string? NombreCliente { get; set; }  // Puede ser NULL

        public string? MotivoIngreso { get; set; }  // Puede ser NULL

        public bool GarantiaConLocal { get; set; }  // Nota: Este campo sigue siendo NOT NULL en la base de datos

        public string? ContraseñaEquipo { get; set; }  // Puede ser NULL

        public string? Descripcion { get; set; }  // Puede ser NULL

        public DateTime? FechaIngreso { get; set; }  // Puede ser NULL

        public int? UsuarioId { get; set; }  // Puede ser NULL, ya está alineado con la base de datos

        public virtual ICollection<Aprobacione> Aprobaciones { get; set; } = new List<Aprobacione>();

        // Relación opcional con la entidad User
        public virtual User? Usuario { get; set; }  // Relación opcional

        // Relación con HistorialEquipo
        public virtual ICollection<HistorialEquipo> HistorialEquipos { get; set; } = new List<HistorialEquipo>();
    }
}
