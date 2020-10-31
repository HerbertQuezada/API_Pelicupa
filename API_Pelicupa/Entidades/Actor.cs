using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Entidades
{
    public class Actor:IID
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public List<PeliculaActor> PeliculaActores { get; set; }
    }
}
