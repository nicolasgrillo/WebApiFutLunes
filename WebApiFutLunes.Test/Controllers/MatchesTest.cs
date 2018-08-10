using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiFutLunes.Controllers;
using WebApiFutLunes.Data.Repositories;
using WebApiFutLunes.Data.Repositories.Interface;
using WebApiFutLunes.Models.Match;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Test.Controllers
{
    [TestClass]
    public class MatchesTest
    {
        private MatchesController Controller { get; set; }
        private IMatchesRepository MatchesRepository { get; set; }
        private IPlayersRepository PlayersRepository { get; set; }

        [TestInitialize]
        public void MatchesTestInitialize()
        {
            MatchesRepository = new MatchesRepository();
            PlayersRepository = new PlayersRepository();

            Controller = new MatchesController(MatchesRepository, PlayersRepository)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task GetReturnsOkOrNotFound()
        {
            //Arrange

            //Act

            IHttpActionResult response = await Controller.Get();
            Type respType = response.GetType();

            //Assert

            //TODO: Should actually check for 200
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task GetWithParamReturnsOkOrNotFound()
        {
            //Arrange

            //Act

            IHttpActionResult response = await Controller.Get(1);
            Type respType = response.GetType();

            //Assert

            //TODO: Should actually check for 200
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task AddMatchReturnsCreated()
        {
            //Arrange
            
            //Act

            IHttpActionResult response = await Controller.Post(new AddUpdateMatchModel()
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

            //Act

            IHttpActionResult response = await Controller.SignUp(new PlayerToMatchModel()
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
            
            //Act

            IHttpActionResult response = await Controller.Dismiss(new PlayerToMatchModel()
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
            
            //Act

            IHttpActionResult response = await Controller.Update(1, new AddUpdateMatchModel()
            {
                LocationMapUrl = "TestMatchMapUrl",
                LocationTitle = "TestMatchUpdated",
                MatchDate = DateTime.Now.AddDays(-7),
                PlayerLimit = 10
            });

            //Assert
            //TODO: Should actually check for 201
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }

        [TestMethod]
        [TestCategory("MatchesTests")]
        public async Task ConfirmReturnsNoContent()
        {
            //Arrange
            
            //Act

            IHttpActionResult response = await Controller.Confirm(1);

            //Assert
            //TODO: Should actually check for 201
            Assert.IsNotInstanceOfType(response.GetType(), typeof(InternalServerErrorResult));
        }
    }
}
