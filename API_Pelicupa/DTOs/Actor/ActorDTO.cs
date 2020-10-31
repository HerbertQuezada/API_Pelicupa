using API_Pelicula.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pelicula.DTOs.Actor
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [NotMapped]
        public int Edad {
            get
            {
                double edad = (DateTime.Now - FechaNacimiento).TotalDays/365;
                return (int)Math.Floor(edad);
            }
        }
        public string Foto { get; set; }
    }
}
