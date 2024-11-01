using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APS.Data.Models
{
    public partial class DetalleFactura
    {
        [Key]
        public int DetalleID { get; set; }

        [ForeignKey("Factura")]
        public int FacturaID { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }

        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;


        public Factura Factura { get; set; }
    }
}
