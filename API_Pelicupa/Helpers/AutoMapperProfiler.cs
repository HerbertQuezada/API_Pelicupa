using API_Pelicula.DTOs.Actor;
using API_Pelicula.DTOs.Genero;
using API_Pelicula.DTOs.Pelicula;
using API_Pelicula.DTOs.Review;
using API_Pelicula.DTOs.SalaDeCine;
using API_Pelicula.DTOs.Seguridad;
using API_Pelicula.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Helpers
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler(GeometryFactory geometryFactory)
        {
            CreateMap<IdentityUser, UsuarioDTO>();

            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<Genero, GeneroCrearDTO>().ReverseMap();

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap(); 
            CreateMap<Pelicula, PeliculaDetallesDTO > ()
                .ForMember(x => x.PeliculaGeneros, options => options.MapFrom(MapPeliculaGenero))
                .ForMember(x => x.PeliculaActores, options => options.MapFrom(MapPeliculaActor));

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculaGeneros, options => options.MapFrom(MapPeliculaGenero))
                .ForMember(x => x.PeliculaActores, options => options.MapFrom(MapPeliculaActor));

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Actor, ActorCrearDTO>().ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorCrearDTO, Actor>().ForMember(x => x.Foto, options => options.Ignore());

            CreateMap<SalaDeCine, SalaDeCineDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

            CreateMap<SalaDeCineCrearDTO, SalaDeCine>()
                .ForMember(
                    x => x.Ubicacion, 
                    x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitud ,y.Latitud)))
                );

            CreateMap<ReviewCrearDTO, Review>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Review, ReviewDTO>().ForMember(x => x.NombresUsuario, x => x.MapFrom(y => y.Usuario.UserName));

        }
        private List<GeneroDTO> MapPeliculaGenero(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();

            if (pelicula.PeliculaGeneros == null) return resultado;

            foreach (var generoPelicula in pelicula.PeliculaGeneros)
            {
                resultado.Add(new GeneroDTO() { Id = generoPelicula.GeneroId, Nombre = generoPelicula.Genero.Nombre });
            }

            return resultado;
        }
        private List<ActorPeliculaDetalleDTO> MapPeliculaActor(Pelicula pelicula, PeliculaDetallesDTO PeliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();

            if (pelicula.PeliculaActores == null) return resultado;

            foreach (var peliculaActor in pelicula.PeliculaActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO { ActorId = peliculaActor.ActorId, NombrePersona = peliculaActor.Actor.Nombre, Personaje = peliculaActor.Personaje});
            }

            return resultado;
        }

        private List<PeliculaGenero> MapPeliculaGenero(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculaGenero>();

            if (peliculaCreacionDTO.GeneroIDs == null) return resultado;

            foreach (var id in peliculaCreacionDTO.GeneroIDs)
            {
                resultado.Add(new PeliculaGenero() { GeneroId = id });
            }

            return resultado;
        }
        private List<PeliculaActor> MapPeliculaActor(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculaActor>();

            if (peliculaCreacionDTO.Actor == null) return resultado;

            foreach (var actor in peliculaCreacionDTO.Actor)
            {
                resultado.Add(new PeliculaActor { ActorId = actor.ActorId, Personaje = actor.Personaje});
            }

            return resultado;
        }

    }
}
