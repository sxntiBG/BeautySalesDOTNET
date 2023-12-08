using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agrego las referencias

using BeautySales.BLL.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using BeautySales.Entity;
using BeautySales.DAL.Interfaces;

namespace BeautySales.BLL.Implementacion
{
    public class FireBaseService : IFireBaseService
    {
        //Agrego el contexto
        private readonly IGenericRepository<Configuracion> _repositorio;

        //Constructor

        public FireBaseService(IGenericRepository<Configuracion> repositorio)
        {
           _repositorio = repositorio;
        }

        //Metodo para subir imagenes a FireBase
        public async Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo)
        {
            string UrlImagen = "";

            //Logica para subir la imagen
            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));

                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .PutAsync(StreamArchivo, cancellation.Token);

                UrlImagen = await task;

            }
            catch
            {
                UrlImagen = "";
            }

            return UrlImagen;
        }

        //Metodo para eliminar imagenes en FireBase
        public async Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo)
        {
            //Logica para eliminar la imagen
            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));

                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .DeleteAsync();

                await task;

                return true;

            }
            catch
            {
                return false;
            }
        }

       
    }
}
