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
using WebApiFutLunes.Models.Player;

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
            //TODO: Should actually check for 201
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task SignUpReturnsNoContent()
        {
            //Arrange

            var controller = new MatchesController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act

            IHttpActionResult response = await controller.SignUp(new PlayerToMatchModel()
            {
                MatchId = 1,
                SubscriptionDate = DateTime.Now,
                //TODO: Should seed Admin and use that user
                UserName = "nicogri"
            });

            //Assert
            //TODO: Should actually check for either 400 or 201
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task DismissReturnsNoContent()
        {
            //Arrange

            var controller = new MatchesController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act

            IHttpActionResult response = await controller.Dismiss(new PlayerToMatchModel()
            {
                MatchId = 1,
                SubscriptionDate = DateTime.Now,
                //TODO: Should seed Admin and use that user
                UserName = "nicogri"
            });

            //Assert
            //Should actually check for 201
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task UpdateReturnsNoContent()
        {
            //Arrange

            var controller = new MatchesController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act

            IHttpActionResult response = await controller.Update(1, new AddUpdateMatchModel()
            {
                LocationMapUrl = "TestMatchMapUrl",
                LocationTitle = "TestMatchUpdated",
                MatchDate = DateTime.Now,
                PlayerLimit = 10
            });

            //Assert
            //Should actually check for 201
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }
    }
}
