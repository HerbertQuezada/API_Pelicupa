using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Pelicula
{
    public class FiltroPeliculaDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistroPorPagina = 10;

        public PaginacionDTO Paginacion { 
            get {
                return new PaginacionDTO() { Pagina = Pagina, Cantidad = CantidadRegistroPorPagina };        
            }
        }

        public string Titulo { get; set; }
        public int GenerosId { get; set; }
        public bool EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }
    }
}
