using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego referencias

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BeautySales.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using BeautySales.DAL.Implementacion;
using BeautySales.DAL.Interfaces;
using BeautySales.BLL.Implementacion;
using BeautySales.BLL.Interfaces;

namespace BeautySales.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BeautySalesContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("conexion"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            //Dependencia del envio de correo
            services.AddScoped<ICorreoService, CorreoService>();

            //Dependencia para guardar la multimedia
            services.AddScoped<IFireBaseService, FireBaseService>();

            //Dependencia para generar y encriptar clave
            services.AddScoped<IUtilidadesService, UtilidadesService>();

            //Dependencia para listar roles
            services.AddScoped<IRolService, RolService>();

            //Dependencia de los metodos de usuario
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
