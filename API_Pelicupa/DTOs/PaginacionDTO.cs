using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int CantidadDePagina = 10;
        private readonly int CantidadMaximaDePagina = 50;

        public int Cantidad {
            get {
                return CantidadDePagina;
            }
            set {
                CantidadDePagina = (value <= CantidadMaximaDePagina) ? value : CantidadMaximaDePagina; 
            }
        }
    }
}
