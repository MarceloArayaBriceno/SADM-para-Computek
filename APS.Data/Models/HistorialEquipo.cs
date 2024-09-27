namespace APS.Data.Models
{
    public partial class HistorialEquipo
    {
        public int HistorialId { get; set; }
        public int EquipoId { get; set; }
        public string DescripcionCambio { get; set; }
        public DateTime FechaCambio { get; set; }

        public virtual Equipo Equipo { get; set; }
    }
}