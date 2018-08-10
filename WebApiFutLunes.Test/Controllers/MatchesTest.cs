using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiFutLunes.Controllers;
using WebApiFutLunes.Data.Entities;

namespace WebApiFutLunes.Test.Controllers
{
    [TestClass]
    public class MatchesTest
    {
        [TestMethod]
        public async Task GetReturnsOkOrNotFound()
        {
            //Arrange

            var controller = new MatchesController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act

            IHttpActionResult response = await controller.Get(1);
            Type respType = response.GetType();

            //Assert

            Assert.IsTrue(
                respType == typeof(OkResult) ||
                respType == typeof(NotFoundResult)
            );
        }
    }
}
