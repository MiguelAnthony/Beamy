using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProyBeamyMusic.Controllers;

namespace ProyBeamyMusic.Test
{
    [TestFixture]
    class Home_Test
    {
        [Test]
        public void HomeIndexTest()
        {
            var controller = new HomeController();
            var view = controller.Index() as ViewResult;
            var status = view.StatusCode = 200;

            Assert.AreEqual(status,200);
            Assert.AreEqual("Index",view.ViewName);
        }
    }
}
