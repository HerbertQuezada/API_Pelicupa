using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API_Pelicula.DTOs;
using API_Pelicula.DTOs.Actor;
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
    public class ActoresController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos):base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        [HttpGet("ListaActores")]
        public async Task<ActionResult<List<ActorDTO>>> ListaActores([FromQuery] PaginacionDTO paginacionDTO) 
        {
            return await Get<Actor, ActorDTO>(paginacionDTO);
        }

        [HttpGet("{id:int}", Name = "ObtenerActor")]
        public async Task<ActionResult<ActorDTO>> Actor(int id)
        {
            return await Get<Actor, ActorDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCrearDTO actorDto)
        {
            var entidad = mapper.Map<Actor>(actorDto);

            if (actorDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorDto.Foto.FileName);

                    entidad.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, actorDto.Foto.ContentType);
                }
            }

            await context.AddAsync(entidad);
            await context.SaveChangesAsync();

            var actor = mapper.Map<ActorDTO>(entidad);

            return new CreatedAtRouteResult("ObtenerActor", new { id = actor.Id }, actor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCrearDTO actorDto)
        {

            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDB == null) return NotFound();

            actorDB = mapper.Map(actorDto, actorDB);

            var entidad = mapper.Map<Actor>(actorDto);

            if (actorDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorDto.Foto.FileName);

                    actorDB.Foto = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,actorDB.Foto, actorDto.Foto.ContentType);
                }
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorCrearDTO> patchDocument)
        {
            return await Patch<Actor, ActorCrearDTO>(id, patchDocument);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actorDB.Foto != null)
            { 
                await almacenadorArchivos.BorrarArchivo(contenedor, actorDB.Foto); 
            }

            context.Remove(actorDB);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
