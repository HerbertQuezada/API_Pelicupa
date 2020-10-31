using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Entidades
{
    public class SalaDeCine : IID
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; }
        public Point Ubicacion { get; set; }
        public List<PeliculaSalaDeCine> PeliculaSalaDeCines { get; set; }
    }
}
