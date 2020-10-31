using API_Pelicula.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API_Pelicula
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculaActor>().HasKey(x => new { x.ActorId, x.PeliculaId });
            modelBuilder.Entity<PeliculaGenero>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            modelBuilder.Entity<PeliculaSalaDeCine>().HasKey(x => new { x.PeliculaID, x.SalaDeCineID });
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<SalaDeCine> SalasDeCine { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PeliculaGenero> PeliculasGeneros { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<PeliculaSalaDeCine> PeliculaSalaDeCines { get; set; }
    }
}
