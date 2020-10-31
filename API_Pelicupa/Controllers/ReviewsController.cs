using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_Pelicula.DTOs;
using API_Pelicula.DTOs.Review;
using API_Pelicula.Entidades;
using API_Pelicula.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Pelicula.Controllers
{
    [Route("api/Peliculas/{PeliculaId:int}/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(PeliculaExisteAttribute))]//Para aplicar el filtro
    public class ReviewsController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReviewsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get(int PeliculaId, [FromQuery] PaginacionDTO paginacionDTO) {

            var queryable = context.Reviews.Include(x => x.Usuario).AsQueryable();
            queryable = queryable.Where(x => x.PeliculaId == PeliculaId);

            return await Get<Review, ReviewDTO>(paginacionDTO, queryable);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int PeliculaId, [FromBody] ReviewCrearDTO reviewCrearDTO)
        {
            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var existeReview   = await context.Reviews.AnyAsync(x => x.PeliculaId == PeliculaId && x.UsuarioId == usuarioId);

            if (existeReview)    return BadRequest("El usuario ya a escrito un Review de la pelicula");

            var review        = mapper.Map<Review>(reviewCrearDTO);
            review.PeliculaId = PeliculaId;
            review.UsuarioId  = usuarioId;

            context.Add(review);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{reviewId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int PeliculaId, int reviewId, [FromBody] ReviewCrearDTO reviewCrearDTO)
        {
            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var reviewDB = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewDB == null) return NotFound();
            if (reviewDB.UsuarioId != usuarioId) return Forbid();

            reviewDB = mapper.Map(reviewCrearDTO, reviewDB);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int PeliculaId, int reviewId) {

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var reviewDB = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewDB == null) return NotFound();
            if (reviewDB.UsuarioId != usuarioId) return Forbid();
            
            context.Remove(reviewDB);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
