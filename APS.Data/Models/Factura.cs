
using System.ComponentModel.DataAnnotations;
namespace APS.Data.Models
{
    public partial class Factura
    {
       public Factura()
        {
            DetallesFactura = new List<DetalleFactura>();
        }

        [Key]
        public int FacturaID { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCliente { get; set; }

        [StringLength(100)]
        public string DireccionCliente { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public List<DetalleFactura> DetallesFactura { get; set; }
    }
}
