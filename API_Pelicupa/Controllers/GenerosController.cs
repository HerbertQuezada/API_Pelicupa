using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Pelicula.DTOs;
using API_Pelicula.DTOs.Genero;
using API_Pelicula.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Pelicula.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : CustomBaseController
    {
        public GenerosController(ApplicationDbContext context, IMapper mapper): base(context, mapper)
        {
            
        }

        [HttpGet]
        [HttpGet("ListaGenero")]
        public async Task<ActionResult<List<GeneroDTO>>> ListaGenero()
        {
            return await Get<Genero, GeneroDTO>();
        }

        [HttpGet("{id:int}", Name = "ObtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Genero(int id)
        {
            return await Get<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCrearDTO generoDto)
        {
            return await Post<GeneroCrearDTO, Genero, GeneroDTO>(generoDto, "ObtenerGenero");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] GeneroDTO generoDto)
        {
            return await Put<Genero, GeneroDTO>(Id, generoDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Genero>(id);
        }
    }
}
