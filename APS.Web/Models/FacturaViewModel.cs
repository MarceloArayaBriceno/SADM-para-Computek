namespace APS.Web.Models
{
    public class FacturaViewModel
    {
        public int FacturaID { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleFacturaViewModel> DetallesFactura { get; set; }

        public FacturaViewModel()
        {
            DetallesFactura = new List<DetalleFacturaViewModel>();
        }
    }
}
