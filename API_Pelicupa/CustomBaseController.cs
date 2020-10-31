using API_Pelicula.DTOs;
using API_Pelicula.Entidades;
using API_Pelicula.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().ToListAsync();
            var dto = mapper.Map<List<TDTO>>(entidad);

            return dto;
        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IID
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var dto = mapper.Map<TDTO>(entidad);
            return dto;
        }

        protected async Task<ActionResult<List<TDTO>>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO) where TEntidad : class
        {
            var queryable = context.Set<TEntidad>().AsQueryable();

            return await Get<TEntidad, TDTO>(paginacionDTO, queryable);
        }

        protected async Task<ActionResult<List<TDTO>>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO, IQueryable<TEntidad> queryable) where TEntidad : class
        {
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.Cantidad);

            var lista = await queryable.Paginar(paginacionDTO).ToListAsync();

            return mapper.Map<List<TDTO>>(lista);
        }

        protected async Task<ActionResult> Post<TCreacion, TEntidad, TLectura>(TCreacion CreacionDTO, string nombreRuta) where TEntidad : class, IID
        {
            var entidad = mapper.Map<TEntidad>(CreacionDTO);

            await context.AddAsync(entidad);
            await context.SaveChangesAsync();

            var dto = mapper.Map<TLectura>(entidad);

            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, dto);
        }

        protected  async Task<ActionResult> Put<TEntidad, TCreacion>(int Id, TCreacion DTO) where TEntidad: class, IID
        {
            var entidad = mapper.Map<TEntidad>(DTO);
            entidad.Id = Id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        protected async Task<ActionResult> Patch<TEntidad, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument) where TEntidad : class, IID where TDTO : class
        {
            if (patchDocument == null) return BadRequest();

            var entidadDB = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null) return NotFound();

            var entidadDTO = mapper.Map<TDTO>(entidadDB);

            patchDocument.ApplyTo(entidadDTO, ModelState);

            var EsValido = TryValidateModel(entidadDTO);

            if (!EsValido) return BadRequest(ModelState);

            mapper.Map(entidadDTO, entidadDB);

            await context.SaveChangesAsync();

            return NoContent();
        }

        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IID, new ()
        {
            var existe = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existe == null) return NotFound();

            context.Remove(new TEntidad() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
