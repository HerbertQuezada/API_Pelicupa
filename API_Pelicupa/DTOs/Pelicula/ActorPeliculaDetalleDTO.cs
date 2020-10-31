using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Pelicula
{
    public class ActorPeliculaDetalleDTO
    {
        public int ActorId { get; set; }
        public string Personaje { get; set; }
        public string NombrePersona { get; set; }
    }
}
