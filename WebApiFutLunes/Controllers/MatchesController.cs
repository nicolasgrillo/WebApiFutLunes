using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.Entities;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Models.Match;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Controllers
{
    [RoutePrefix("api/matches")]
    public class MatchesController : ApiController
    {
        private ApplicationDbContext _context { get; set; }

        public MatchesController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _context.Matches.ToListAsync();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _context.Matches.FindAsync(id);
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
                Players = new List<ApplicationUser>()
            };

            _context.Matches.Add(match);
            if (await _context.SaveChangesAsync() <= 0)
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


            var match = _context.Matches.FirstOrDefault(m => m.Id == transaction.MatchId);
            if (match == null)
            {
                return NotFound();
            }
            else { 
                var playerEntity = _context.Users.FirstOrDefault(p => p.UserName == transaction.UserName);
                if (playerEntity == null)
                {
                    return NotFound();
                }

                if (match.Players.Contains(playerEntity))
                {
                    return BadRequest("Player is already in the match");
                }
                else
                {
                    //TODO: Should enable match transactions only for future matches
                    match.Players.Add(playerEntity);
                    playerEntity.Appearances++;
                }
            }

            if (await _context.SaveChangesAsync() <= 0)
            {
                return InternalServerError();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // Add player to match
        // POST api/matches
        [Route("dismiss")]
        public async Task<IHttpActionResult> Dismiss([FromBody] PlayerToMatchModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var match = _context.Matches.FirstOrDefault(m => m.Id == transaction.MatchId);
            if (match == null)
            {
                return NotFound();
            }
            else
            {
                var playerEntity = _context.Users.FirstOrDefault(p => p.UserName == transaction.UserName);
                if (playerEntity == null)
                {
                    return NotFound();
                }

                if (match.Players.Contains(playerEntity))
                {
                    //TODO: Should enable match transactions only for future matches
                    match.Players.Remove(playerEntity);
                    playerEntity.Appearances--;
                }
                else
                {
                    return BadRequest("Player was not signed up for the match");
                }
            }

            if (await _context.SaveChangesAsync() <= 0)
            {
                return InternalServerError();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // Update match
        // PUT api/matches
        public async Task<IHttpActionResult> Put(int id, [FromBody] AddUpdateMatchModel matchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = _context.Matches.FirstOrDefault(m => m.Id == id);

            if (match != null)
            {
                match.MatchDate = matchModel.MatchDate;
                match.LocationMapUrl = matchModel.LocationMapUrl;
                match.LocationTitle = matchModel.LocationTitle;
                match.PlayerLimit = matchModel.PlayerLimit;

                if (await _context.SaveChangesAsync() <= 0)
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
