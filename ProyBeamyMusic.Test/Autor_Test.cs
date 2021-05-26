using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ProyBeamyMusic.Test
{
    [TestFixture]
    class Autor_Test
    {
        [Test]
        public void CrearArtista()
        {
            var repo = new Mock<IAdminRepo>();
            repo.Setup(o => o.SaveArtista(new Artista(), null));

            var controller = new AdminController(repo.Object);
            var view = controller.CrearArtista(new Artista(), null) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }



    }
}
