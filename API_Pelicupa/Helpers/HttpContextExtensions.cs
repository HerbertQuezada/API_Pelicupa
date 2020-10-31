using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable, int cantidadRegistroPorPagina)
        {
            double cantidad = await queryable.CountAsync();
            double cantidadPagina = Math.Ceiling(cantidad / cantidadRegistroPorPagina);
            httpContext.Response.Headers.Add("CantidadPaginas", cantidadPagina.ToString());
        }
    }
}
