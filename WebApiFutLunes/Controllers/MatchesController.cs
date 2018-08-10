using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.Entities;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Repositories.Interface;
using WebApiFutLunes.Models.Match;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Controllers
{
    [RoutePrefix("api/matches")]
    public class MatchesController : ApiController
    {
        private ApplicationDbContext _context { get; set; }
        private IMatchesRepository _matchesRepo { get; set; }
        private IPlayersRepository _playersRepo { get; set; }

        public MatchesController(IMatchesRepository matchesRepo, IPlayersRepository playersRepo)
        {
            _matchesRepo = matchesRepo;
            _playersRepo = playersRepo;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _matchesRepo.GetAllMatchesAsync();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _matchesRepo.GetMatchByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Add new match
        // POST api/matches
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
                Players = new List<UserSubscription>()
            };
            
            if (await _matchesRepo.AddMatchAsync(match) <= 0)
            {
                return InternalServerError();
            }
            return StatusCode(HttpStatusCode.Created);
        }

        // Add player to match
        // POST api/matches
        [Route("signup")]
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

                var playerDto = new UserSubscription()
                {
                    SubscriptionDate = DateTime.Now,
                    User = playerEntity
                };

                if (match.Players.Any(p => p.User.UserName == playerDto.User.UserName))
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

        // Remove player from match
        // POST api/matches
        [Route("dismiss")]
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

                var subscription = match.Players.SingleOrDefault(p => p.User.UserName == playerEntity.UserName);
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

        // Update match
        // PUT api/matches
        // TODO: Check admin role
        [HttpPut]
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

        // Confirm match
        // POST api/matches/{id}/confirm
        [Route("{id}/confirm")]
        public async Task<IHttpActionResult> Confirm(int id)
        {
            var match = await _matchesRepo.GetMatchByIdAsync(id);

            if (match != null)
            {
                if (match.MatchDate.CompareTo(DateTime.Now) >= 1)
                    return BadRequest("Match must have happened to be confirmed.");

                
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
    }
}
