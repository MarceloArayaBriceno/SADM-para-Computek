namespace APS.Web.Models
{
    public class DetalleFacturaViewModel
    {
        public int DetalleID { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
