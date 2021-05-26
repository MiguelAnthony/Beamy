using System;
using System.Collections.Generic;
using System.Text;
using BeamyMusic.Controllers;
using BeamyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProyBeamyMusic.Repositorio;

namespace ProyBeamyMusic.Test
{
    [TestFixture]
    class Admin_Test
    {
        [Test]
        public void IndexTest()
        {
            var repo = new Mock<IAdminRepo>();
            repo.Setup(o => o.GetCanciones()).Returns(new List<Cancion>());
            repo.Setup(o => o.GetAtistas()).Returns(new List<Artista>());
            repo.Setup(o => o.GetAlbunes()).Returns(new List<Album>());

            var controller = new AdminController(repo.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }
        [Test]
        public void IndexTestPost()
        {
            var repo = new Mock<IAdminRepo>();
            repo.Setup(o => o.SaveCancion(new Cancion(), null, null));
           
            var controller = new AdminController(repo.Object);
            var view = controller.Index(new Cancion(), null, null) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CrearArtista()
        {
            var repo = new Mock<IAdminRepo>();
            repo.Setup(o => o.SaveArtista (new Artista(), null));

            var controller = new AdminController(repo.Object);
            var view = controller.CrearArtista(new Artista(), null) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CrearAlbum()
        {
            var repo = new Mock<IAdminRepo>();
            repo.Setup(o => o.SaveAlbum(new Album(), null));

            var controller = new AdminController(repo.Object);
            var view = controller.CrearAlbum(new Album(), null) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void PlayList()
        {
            var repo = new Mock<IAdminRepo>();
            repo.Setup(o => o.SavePlayList(new PlayList(), null));

            var controller = new AdminController(repo.Object);
            var view = controller.PlayList(new PlayList(), null) as RedirectToActionResult;

            Assert.AreEqual("CrearPlayList", view.ActionName);
        }

    }
}
