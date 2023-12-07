using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego referencias
using BeautySales.Entity;

namespace BeautySales.DAL.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta entidad);
        Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin);
    }
}
