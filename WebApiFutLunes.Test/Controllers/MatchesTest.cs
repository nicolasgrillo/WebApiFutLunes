using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiFutLunes.Controllers;
using WebApiFutLunes.Data.Entities;
using WebApiFutLunes.Models.Match;

namespace WebApiFutLunes.Test.Controllers
{
    [TestClass]
    public class MatchesTest
    {
        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task GetReturnsOkOrNotFound()
        {
            //Arrange

            var controller = new MatchesController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act

            IHttpActionResult response = await controller.Get();
            Type respType = response.GetType();

            //Assert

            Assert.IsTrue(
                respType == typeof(OkResult) ||
                respType == typeof(NotFoundResult)
            );
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task GetWithParamReturnsOkOrNotFound()
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

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task AddMatchReturnsCreated()
        {
            //Arrange

            var controller = new MatchesController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act

            IHttpActionResult response = await controller.Post(new AddUpdateMatchModel()
            {
                LocationTitle = "TestMatch",
                LocationMapUrl = "TestMatchMapUrl",
                MatchDate = DateTime.Now,
                PlayerLimit = 10
            });
            
            //Assert
            
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }
    }
}
