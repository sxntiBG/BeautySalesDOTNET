using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego referencias
using BeautySales.DAL.DBContext;
using BeautySales.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace BeautySales.DAL.Implementacion
{
    //Implementamos las interfaces
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //Implemento el contexto
        private readonly BeautySalesContext _dbContext;

        //Creo constructor
        public GenericRepository(BeautySalesContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Metodo para obtener
        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        //Metodo para crear
        public async Task<TEntity> Crear(TEntity entidad)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        //Metodo para editar
        public async Task<bool> Editar(TEntity entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //Metodo para eliminar
        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                _dbContext.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //Metodo para consultar
        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            IQueryable<TEntity> queryEntidad = filtro == null ? _dbContext.Set<TEntity>(): _dbContext.Set<TEntity>().Where(filtro);
            return queryEntidad;
        } 
    }
}
