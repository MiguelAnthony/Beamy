using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeamyMusic.DataBase;
using BeamyMusic.Models;
using FinancistoCloneWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProyBeamyMusic.Repositorio;

namespace BeamyMusic.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminRepo context;

        public AdminController(IAdminRepo context)
        {
            this.context = context;
        }

       
        [HttpGet]
        public ActionResult Index()
        {
            var canciones = context.GetCanciones();
            ViewBag.Artista = context.GetAtistas();
            ViewBag.Album = context.GetAlbunes();
            return View("Index", new Cancion());
        }
        [HttpPost]
        public ActionResult Index(Cancion canciones, IFormFile Cancion, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                context.SaveCancion(canciones,Cancion,Foto);
                return RedirectToAction("Index");

            }
            else
            {

                ViewBag.Artista = context.GetAtistas();
                ViewBag.Album = context.GetAlbunes();
                return View("Index", canciones);
            }
        }
        [Route("Exito")]
        public IActionResult Exito()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CrearArtista()
        {
            return View(new Artista());
        }
        [HttpPost]
        public ActionResult CrearArtista(Artista artistas, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                context.SaveArtista(artistas,Foto);
                return RedirectToAction("Index");

            }
            else
            {
                return View("CrearArtista", artistas);
            }
        }
        [HttpGet]
        public ActionResult CrearAlbum()
        {
            return View(new Album());
        }
        [HttpPost]
        public ActionResult CrearAlbum(Album albumes, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                context.SaveAlbum(albumes,Foto);
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Artista = context.GetAtistas();
                ViewBag.Album = context.GetAlbunes();
                return View("CrearAlbum", albumes);
            }
        }
        public ActionResult CrearPlayList()
        {
            ViewBag.canciones = context.GetCancionesPL();
            return View();
        }
        [HttpGet]
        public ActionResult PlayList()
        {
            ViewBag.playList = context.GetPlayList();
            
            return View(new PlayList());
        }
        [HttpPost]
        public ActionResult PlayList(PlayList playList, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                context.SavePlayList(playList, Foto);
                return RedirectToAction("CrearPlayList");

            }
            else
            {
                ViewBag.playList = context.GetUsuarios()
                    .Where(o => o.Id == LoggedUser().Id)
                    .ToList();
                return View("PlayList", playList);
            }
        }
        [HttpGet]
        public IActionResult AgregarPorDefecto(int Id)
        {
          
            ViewBag.paseId = Id;
            ViewBag.playList = context.GetPlayList();
            return View();
        }
     
        [HttpGet]
        public IActionResult AgregarPorDefecto2(int cancion, int playlist)
        {
            context.SaveDefect(cancion, playlist);
            return RedirectToAction("Index");
        }

        protected Usuario LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = context.GetUsuarios().Where(o => o.Nick == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
