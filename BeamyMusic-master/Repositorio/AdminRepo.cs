using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.DataBase;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ProyBeamyMusic.Repositorio
{
    public interface IAdminRepo
    {
        List<Album> GetAlbunes();
        List<Artista> GetAtistas();
        List<Cancion> GetCanciones();
        List<PlayList> GetPlayList();
        List<Cancion> GetCancionesPL();
        List<Usuario> GetUsuarios();
        void SaveDefect(int cancion, int playlist);
        void SaveCancion(Cancion canciones, IFormFile Cancion, IFormFile Foto);
        void SaveArtista(Artista artistas, IFormFile Foto);
        void SaveAlbum(Album albumes, IFormFile Foto);
        void SavePlayList(PlayList playList, IFormFile Foto);

    }
    public class AdminRepo: IAdminRepo
    {
        private readonly IBeamyContext context;
        private readonly IWebHostEnvironment hosting;

        public AdminRepo(IBeamyContext context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }

        public List<Album> GetAlbunes()
        {
            return context.Albumes.ToList();
        }

        public List<Artista> GetAtistas()
        {
            return context.Artistas.ToList();
        }

        public List<Cancion> GetCanciones()
        {
            return context.Canciones
                 .Include(o => o.Artistas)
                 .Include(x => x.Albumes)
                 .ToList();
        }

        public List<PlayList> GetPlayList()
        {
            return context.PlayListas
                .ToList();
        }

        public List<Usuario> GetUsuarios()
        {
            return context.Usuarios
                .Include(x => x.PlayListas)
                .ToList();
        }
        public void SaveCancion(Cancion canciones, IFormFile Cancion, IFormFile Foto)
        {
            canciones.LinkDeCancion = SaveCancion(Cancion);
            canciones.Foto = SaveFoto(Foto);
            context.Canciones.Add(canciones);
            context.SaveChanges();
        }

        public void SaveArtista(Artista artistas, IFormFile Foto)
        {
            artistas.Foto = SaveFoto(Foto);
            context.Artistas.Add(artistas);
            context.SaveChanges();
        }

        public void SaveAlbum(Album albumes, IFormFile Foto)
        {
            albumes.Foto = SaveFoto(Foto);
            context.Albumes.Add(albumes);
            context.SaveChanges();
        }

        public List<Cancion> GetCancionesPL()
        {
            return context.Canciones
                 .Include(o => o.PlayListCanciones)
                 .Include(x => x.Favoritos)
                 .ToList();
        }

        public void SaveDefect(int cancion, int playlist)
        {
            var listita = new PlayListCancion
            {
                IdPlayList = playlist,
                IdCancion = cancion
            };
            context.PlayListCanciones.Add(listita);
            context.SaveChanges();
        }

        public void SavePlayList(PlayList playList, IFormFile Foto)
        {
            playList.Foto = SaveFoto(Foto);
            context.PlayListas.Add(playList);
            context.SaveChanges();
        }

        private string SaveCancion(IFormFile Cancion)
        {
            if (Cancion != null && Cancion.Length > 0)
            {
                var basePath = hosting.ContentRootPath + @"\wwwroot";
                var ruta = @"\Music\" + Cancion.FileName;

                using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                {
                    Cancion.CopyTo(strem);
                    return ruta;
                }
            }
            return "/";
        }

        private string SaveFoto(IFormFile Foto)
        {
            if (Foto != null && Foto.Length > 0)
            {
                var basePath = hosting.ContentRootPath + @"\wwwroot";
                var ruta = @"\FtCancion\" + Foto.FileName;

                using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                {
                    Foto.CopyTo(strem);
                    return ruta;
                }
            }
            return "/";
        }
    }
}
