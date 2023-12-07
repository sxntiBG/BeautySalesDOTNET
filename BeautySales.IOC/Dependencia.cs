﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego referencias

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BeautySales.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
//using BeautySales.DAL.Implementacion;
//using BeautySales.DAL.Interfaces;
//using BeautySales.BLL.Implementacion;
//using BeautySales.BLL.Interfaces;

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
        }
    }
}