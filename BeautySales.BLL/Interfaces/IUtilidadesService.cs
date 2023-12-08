using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySales.BLL.Interfaces
{
    public interface IUtilidadesService
    {
        //Metodo para generar clave para que el usuario pueda loguearse
        string GenerarClave();

        //Metodo para encriptar en codigo en Sha256
        string ConvertirSha256(string texto);
    }
}
