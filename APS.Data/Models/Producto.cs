using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.Data.Models
{
    public partial class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string RutaImagen {  get; set; }

        public Boolean activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; }= DateTime.Now;

    }
}
