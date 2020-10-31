using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Entidades
{
    public class Pelicula:IID
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
        public List<PeliculaActor> PeliculaActores { get; set; }
        public List<PeliculaGenero> PeliculaGeneros { get; set; }
        public List<PeliculaSalaDeCine> PeliculaSalaDeCines { get; set; }
    }
}
