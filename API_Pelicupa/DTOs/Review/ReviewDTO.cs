using API_Pelicula.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Review
{
    public class ReviewDTO : IID
    {
            public int Id { get; set; }
            public string Comentario { get; set; }
            public int Puntuacion { get; set; }
            public int PeliculaId { get; set; }
            public string UsuarioId { get; set; }
            public string NombresUsuario { get; set; }
    }
}
