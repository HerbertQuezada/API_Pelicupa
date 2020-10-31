using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Pelicula.DTOs.SalaDeCine;
using API_Pelicula.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace API_Pelicula.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasDeCineController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;

        public SalasDeCineController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometryFactory) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        [HttpGet("{id:int}", Name = "ObtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDTO>> Get(int id)
        {
            return await Get<SalaDeCine, SalaDeCineDTO>(id);
        }
        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDTO>>> Get()
        {
            return await Get<SalaDeCine, SalaDeCineDTO>();
        }
        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Get([FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salaDeCine = await context.SalasDeCine
                    .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                    .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKms * 1000))
                    .Select(x => new SalaDeCineCercanoDTO
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Latitud = x.Ubicacion.Y,
                        Longitud = x.Ubicacion.X,
                        DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario))
                    }).ToListAsync();

            return salaDeCine;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaDeCineCrearDTO salaDeCineDto)
        {
            return await Post<SalaDeCineCrearDTO, SalaDeCine, SalaDeCineDTO>(salaDeCineDto, "ObtenerSalaDeCine");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] SalaDeCineDTO salaDeCineDto)
        {
            return await Put<SalaDeCine, SalaDeCineDTO>(Id, salaDeCineDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SalaDeCine>(id);
        }

    }
}
