using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Review
{
    public class ReviewCrearDTO
    {
        public string Comentario { get; set; }
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}
