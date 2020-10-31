using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Genero
{
    public class GeneroCrearDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
