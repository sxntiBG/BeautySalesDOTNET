using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego referencias
using BeautySales.BLL.Interfaces;
using BeautySales.DAL.Interfaces;
using BeautySales.Entity;

namespace BeautySales.BLL.Implementacion
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _repositorio;

        //Constructor
        public RolService(IGenericRepository<Rol> repositorio)
        {
            _repositorio = repositorio;
        }

        //Devolvera la lista de roles
        public async Task<List<Rol>> Lista()
        {
            IQueryable<Rol> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
