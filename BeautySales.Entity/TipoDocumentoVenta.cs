using System;
using System.Collections.Generic;

namespace BeautySales.Entity
{
    public partial class TipoDocumentoVenta
    {
        public TipoDocumentoVenta()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdTipoDocumentoVenta { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
