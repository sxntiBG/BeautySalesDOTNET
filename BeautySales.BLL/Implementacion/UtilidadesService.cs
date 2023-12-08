using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego referencias
using BeautySales.BLL.Interfaces;
using System.Security.Cryptography;

namespace BeautySales.BLL.Implementacion
{
    public class UtilidadesService : IUtilidadesService
    {
        //Logica para generar clave
        public string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6); //Retorna una cadena de texto aleatoria
            return clave;
        }

        //Logica para encriptar la clave
        public string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create()) //Creamos el objeto para encriptar el texto
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();

        }
   
    }
}
