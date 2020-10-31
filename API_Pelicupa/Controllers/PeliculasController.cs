using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API_Pelicula.DTOs;
using API_Pelicula.DTOs.Pelicula;
using API_Pelicula.Entidades;
using API_Pelicula.Helpers;
using API_Pelicula.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Pelicula.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenador;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenador):base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenador = almacenador;
        }

        [HttpGet]
        [HttpGet("ListaPeliculas")]
        public async Task<ActionResult<PeliculaIndexDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var top = 5;
            var hoy = DateTime.Today;

            var EnCines = await context.Peliculas
                            .Where(x => x.FechaEstreno <= hoy)
                            .Take(top)
                            .ToListAsync();
            
            var EnEstreno = await context.Peliculas
                            .Where(x => x.FechaEstreno > hoy)
                            .Take(top)
                            .ToListAsync();

            var resultado = new PeliculaIndexDTO();

            resultado.EnCines = mapper.Map<List<PeliculaDTO>>(EnCines);
            resultado.FuturoEstrenos = mapper.Map<List<PeliculaDTO>>(EnEstreno);

            return resultado;
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] FiltroPeliculaDTO filtroPeliculaDTO)
        {
            var query = context.Peliculas.AsQueryable();

            if (!string.IsNullOrEmpty(filtroPeliculaDTO.Titulo)) query = query.Where(x => x.Titulo.Contains(filtroPeliculaDTO.Titulo));
            
            if (filtroPeliculaDTO.EnCines) query = query.Where(x => x.EnCines);
            
            if (filtroPeliculaDTO.ProximosEstrenos) query = query.Where(x => x.FechaEstreno > DateTime.Today);

            if (filtroPeliculaDTO.GenerosId != 0)
            {
                query = query.Where(x => x.PeliculaGeneros.Select(y => y.GeneroId)
                                                          .Contains(filtroPeliculaDTO.GenerosId));
            }

            await HttpContext.InsertarParametrosPaginacion(query, filtroPeliculaDTO.CantidadRegistroPorPagina);

            var peliculas = await query.Paginar(filtroPeliculaDTO.Paginacion).ToListAsync();

            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }

        [HttpGet("{id:int}", Name = "GetPelicula")]
        public async Task<ActionResult<PeliculaDetallesDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas
                                        .Include(x => x.PeliculaActores).ThenInclude(y => y.Actor)
                                        .Include(x => x.PeliculaGeneros).ThenInclude(y => y.Genero)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula == null) return NotFound();

            pelicula.PeliculaActores = pelicula.PeliculaActores.OrderBy(x => x.Orden).ToList();

            var peliculaDto = mapper.Map<PeliculaDetallesDTO>(pelicula);
            
            return peliculaDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaDto)
        {
            var entidad = mapper.Map<Pelicula>(peliculaDto);

            if (peliculaDto.Poster != null)
            {
                using (var memmoryStream = new MemoryStream())
                {
                    await peliculaDto.Poster.CopyToAsync(memmoryStream);
                    var contenido = memmoryStream.ToArray();
                    var Extension = Path.GetExtension(peliculaDto.Poster.FileName);

                    entidad.Poster = await almacenador.GuardarArchivo(contenido, Extension, contenedor, peliculaDto.Poster.ContentType);
                }
            }

            AsignarOrdenActor(entidad);

            await context.AddAsync(entidad);
            await context.SaveChangesAsync();

            var pelicula = mapper.Map<PeliculaDetallesDTO>(entidad);

            return new CreatedAtRouteResult("GetPelicula", new { id = entidad.Id }, pelicula);
        }

        private void AsignarOrdenActor(Pelicula pelicula) {
            if (pelicula.PeliculaActores != null)
            {
                for (int i = 0; i < pelicula.PeliculaActores.Count; i++)
                {
                    pelicula.PeliculaActores[i].Orden = i;
                }
            }
        } 

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaDto)
        {

            var peliculaDB = await context.Peliculas
                .Include(x => x.PeliculaActores)
                .Include(x => x.PeliculaGeneros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (peliculaDB == null) return NotFound();

            peliculaDB = mapper.Map(peliculaDto, peliculaDB);

            var entidad = mapper.Map<Pelicula>(peliculaDto);

            if (peliculaDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaDto.Poster.FileName);

                    peliculaDB.Poster = await almacenador.EditarArchivo(contenido, extension, contenedor, peliculaDB.Poster, peliculaDto.Poster.ContentType);
                }
            }

            AsignarOrdenActor(peliculaDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaCreacionDTO> patchDocument)
        {
            return await Patch<Pelicula, PeliculaCreacionDTO>(id, patchDocument);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var peliculaDB = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (peliculaDB.Poster != null)
            {
                await almacenador.BorrarArchivo(contenedor, peliculaDB.Poster);
            }

            context.Remove(peliculaDB);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
