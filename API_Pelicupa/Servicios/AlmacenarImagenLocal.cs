using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Servicios
{
    public class AlmacenarImagenLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;//parar poder obtener la ruta del wwwroot
        private readonly IHttpContextAccessor httpContextAccessor;//para determinar el dominio donde esta publicado la api

        public AlmacenarImagenLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");

            return urlParaBD;
        }

        public Task BorrarArchivo(string contenedor, string ruta)
        {
            if (ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string archivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(archivo)) File.Delete(archivo);
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(contenedor, ruta);
            return await GuardarArchivo(contenido, extension,contenedor, contentType);
        }
    }
}
