using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.DataBase.Maps;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyBeamyMusic.Models;

namespace BeamyMusic.DataBase
{
    public interface IBeamyContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Cancion> Canciones { get; set; }
        DbSet<PlayListCancion> PlayListCanciones { get; set; }
        DbSet<Favorito> Favoritos { get; set; }
        DbSet<PlayList> PlayListas { get; set; }
        DbSet<Album> Albumes { get; set; }
        DbSet<Artista> Artistas { get; set; }
        DbSet<DetalleSeguir> DetalleSeguir { get; set; }
        int SaveChanges();
    }
    public class BeamyContext : DbContext, IBeamyContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cancion> Canciones { get; set; }
        public DbSet<PlayListCancion> PlayListCanciones { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<PlayList> PlayListas { get; set; }
        public DbSet<Album> Albumes { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<DetalleSeguir> DetalleSeguir { get; set; }

        public BeamyContext(DbContextOptions<BeamyContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new CancionConfiguration());
            modelBuilder.ApplyConfiguration(new PlayListCancionConfiguration());
            modelBuilder.ApplyConfiguration(new FavoritoConfiguration());
            modelBuilder.ApplyConfiguration(new PlayListConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistaConfiguration());
            modelBuilder.ApplyConfiguration(new DetalleSeguirConfiguration());
        }
    }
}
