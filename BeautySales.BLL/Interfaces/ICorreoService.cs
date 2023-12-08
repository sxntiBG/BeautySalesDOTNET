using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySales.BLL.Interfaces
{
    public interface ICorreoService
    {
        //Metodo para enviar correo
        Task<bool> EnviarCorreo(string CorreoDestino, string Asunto, string Mensaje);
    }
}
