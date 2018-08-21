using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFutLunes.Data.Entities;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Repositories.Interface;
using WebApiFutLunes.Models.Match;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Controllers
{
    [Authorize]
    [RoutePrefix("api/matches")]
    public class MatchesController : ApiController
    {
        private IMatchesRepository _matchesRepo { get; set; }
        private IPlayersRepository _playersRepo { get; set; }

        public MatchesController(IMatchesRepository matchesRepo, IPlayersRepository playersRepo)
        {
            _matchesRepo = matchesRepo;
            _playersRepo = playersRepo;
        }

        // GET api/matches
        /// <summary>
        /// Get all matches
        /// </summary>
        /// <returns>List of all matches or 404</returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _matchesRepo.GetAllMatchesAsync();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/matches/id
        /// <summary>
        /// Get match by id
        /// </summary>
        /// <param name="id">The ID of the match.</param>
        /// <returns>Found match or 404</returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _matchesRepo.GetMatchByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/matches/current
        /// <summary>
        /// Get current match
        /// </summary>
        /// <returns>Last Match (By ID) or 404</returns>
        [Route("current")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetCurrentMatch()
        {
            var result = await _matchesRepo.GetCurrentMatchAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/matches
        /// <summary>
        /// Add new match
        /// </summary>
        /// <param name="matchModel">Model required to create a new Match</param>
        /// <returns>Status 400/500/201</returns>
        [HttpPost]
        [Authorize(Users = "admin")]
        [Route("add")]
        public async Task<IHttpActionResult> Post([FromBody] AddUpdateMatchModel matchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = new Match()
            {
                LocationMapUrl = matchModel.LocationMapUrl,
                LocationTitle = matchModel.LocationTitle,
                MatchDate = matchModel.MatchDate,
                PlayerLimit = matchModel.PlayerLimit,
                Open = true,
                Players = new List<UserSubscription>()
            };
            
            if (await _matchesRepo.AddMatchAsync(match) <= 0)
            {
                return InternalServerError();
            }
            return StatusCode(HttpStatusCode.Created);
        }

        // POST api/matches/signup
        /// <summary>
        /// Add player to match
        /// </summary>
        /// <param name="transaction">Model required to update Player-Match relation</param>
        /// <returns>Status 400/500/404/204</returns>
        [Route("signup")]
        [HttpPost]
        public async Task<IHttpActionResult> SignUp([FromBody] PlayerToMatchModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Match match = await _matchesRepo.GetMatchByIdAsync(transaction.MatchId);
            if (match == null)
            {
                return NotFound();
            }
            else
            {
                var playerEntity = await _playersRepo.GetUserByUsername(transaction.UserName);
                if (playerEntity == null)
                {
                    return NotFound();
                }

                var playerDto = new UserSubscription(playerEntity, DateTime.Now);

                if (match.Players.Any(p => p.User == playerDto.User))
                {
                    return BadRequest("Player is already in the match");
                }
                else
                {
                    if (await _matchesRepo.SignUpPlayerAsync(match, playerDto) <= 0)
                    {
                        return InternalServerError();
                    }
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }
        }

        // POST api/matches/dismiss
        /// <summary>
        /// Remove player from match
        /// </summary>
        /// <param name="transaction">Model required to update Player-Match relation</param>
        /// <returns>Status 400/500/404/204</returns>
        [Route("dismiss")]
        [HttpPost]
        public async Task<IHttpActionResult> Dismiss([FromBody] PlayerToMatchModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var match = await _matchesRepo.GetMatchByIdAsync(transaction.MatchId);
            if (match == null)
            {
                return NotFound();
            }
            else
            {
                var playerEntity = await _playersRepo.GetUserByUsername(transaction.UserName);
                if (playerEntity == null)
                {
                    return NotFound();
                }

                var subscription = match.Players.SingleOrDefault(p => p.User == playerEntity.UserName);
                if (subscription != null)
                {
                    if (await _matchesRepo.DismissPlayerAsync(match, subscription) <= 0)
                    {
                        return InternalServerError();
                    }
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return BadRequest("Player was not signed up for the match");
                }
            }
        }

        // POST api/matches/update
        /// <summary>
        /// Update match
        /// </summary>
        /// <param name="id"></param>
        /// <param name="matchModel">Model required to update Match</param>
        /// <returns>Status 400/500/404/204</returns>
        [Authorize(Users = "admin")]
        [HttpPost]
        [Route("update/{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] AddUpdateMatchModel matchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = await _matchesRepo.GetMatchByIdAsync(id);

            if (match != null)
            {
                //TODO: Following line not working -- AutoMapper
                //AddUpdateMatchDto aumDto = Mapper.Map<AddUpdateMatchModel, AddUpdateMatchDto>(matchModel);

                AddUpdateMatchDto aumDto = new AddUpdateMatchDto()
                {
                    LocationTitle = matchModel.LocationTitle,
                    LocationMapUrl = matchModel.LocationMapUrl,
                    MatchDate = matchModel.MatchDate,
                    PlayerLimit = matchModel.PlayerLimit
                };
                
                if (await _matchesRepo.UpdateMatchAsync(match, aumDto) <= 0)
                {
                    return InternalServerError();
                }
            }
            else
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/matches/{id}/confirm
        /// <summary>
        /// Confirm match
        /// </summary>
        /// <param name="id">Match ID to confirm.</param>
        /// <returns>Status 400/500/404/204</returns>
        [Route("{id}/confirm")]
        [HttpPost]
        public async Task<IHttpActionResult> Confirm(int id)
        {
            var match = await _matchesRepo.GetMatchByIdAsync(id);

            if (match != null)
            {
                if (match.MatchDate.CompareTo(DateTime.Now) >= 1)
                    return BadRequest("Match must have happened to be confirmed.");
                if (!match.Open)
                    return BadRequest("Match is already confirmed");
                
                if (await _matchesRepo.ConfirmMatchAsync(match) <= 0)
                {
                    return InternalServerError();
                }
            }
            else
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/matches/{id}/deconfirm
        /// <summary>
        /// Deconfirm match
        /// </summary>
        /// <param name="id">Match ID to deconfirm.</param>
        /// <returns>Status 400/500/404/204</returns>
        [Route("{id}/deconfirm")]
        [HttpPost]
        public async Task<IHttpActionResult> Deconfirm(int id)
        {
            var match = await _matchesRepo.GetMatchByIdAsync(id);

            if (match != null)
            {
                if (match.Open)
                    return BadRequest("Match is already open");

                if (await _matchesRepo.DeConfirmMatchAsync(match) <= 0)
                {
                    return InternalServerError();
                }
            }
            else
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
