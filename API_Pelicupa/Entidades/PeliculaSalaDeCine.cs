using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Entidades
{
    public class PeliculaSalaDeCine
    {
        public int PeliculaID { get; set; }
        public int SalaDeCineID { get; set; }
        public Pelicula Pelicula { get; set; }
        public SalaDeCine SalaDeCine { get; set; }
    }
}
