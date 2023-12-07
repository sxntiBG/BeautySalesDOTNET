using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego las referencias
using Microsoft.EntityFrameworkCore;
using BeautySales.DAL.DBContext;
using BeautySales.DAL.Interfaces;
using BeautySales.Entity;

namespace BeautySales.DAL.Implementacion
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        //Implementar el contexto

        private readonly BeautySalesContext _dbContext;

        //Constructor

        public VentaRepository(BeautySalesContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;

        }

        //Metodo para registrar venta
        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta ventaGenerada = new Venta();

            //Funcion por si hay un error al hacer una venta se restablesca a como estaba anteriormente
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in entidad.DetalleVenta)
                    {
                        //Si se encuentra el producto que se va a vender se disminuira la cantidad de stock
                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();

                    //Hacemos que al hacerce una venta el numero correlativo de la db se vaya aumentando
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "venta").First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.NumeroVenta = numeroVenta;

                    await _dbContext.Venta.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    ventaGenerada = entidad;

                    transaction.Commit();
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return ventaGenerada; 
        }


        //Metodo para reporte de venta
        public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
                    dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();

            return listaResumen;

        }

    }

}

