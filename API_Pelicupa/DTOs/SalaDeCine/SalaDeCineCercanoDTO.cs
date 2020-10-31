using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.SalaDeCine
{
    public class SalaDeCineCercanoDTO:SalaDeCineDTO
    {
        public double DistanciaEnMetros { get; set; }
    }
}
