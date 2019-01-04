using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentsBack.Data;
using TournamentsBack.Models;

namespace Tournaments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersTournamentsController : Controller
    {
        private readonly TournamentsDbContext _context;

        public UsersTournamentsController(TournamentsDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IEnumerable<UsersTournament> GetUsersTournaments([FromRoute]int id)
        {
            return _context.UsersTournaments
                .Where(ut => ut.TournamentId == id)
                .Include(ut => ut.Tournament)
                .Include(ut => ut.User);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsersTournament([FromBody] UsersTournament usersTournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UsersTournaments.Add(usersTournament);
            await _context.SaveChangesAsync();

            return Ok(usersTournament);
        }
        
        //[Route("UsersTournaments/{userId}/{tournamentId}")]
        [HttpDelete("{userId}/{tournamentId}")]
        public async Task<IActionResult> DeleteUsersTournament([FromRoute] int userId, [FromRoute] int tournamentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTournament = _context.UsersTournaments.Where(ut => ut.UserId == userId && ut.TournamentId == tournamentId).First();

            _context.UsersTournaments.Remove(userTournament);
            await _context.SaveChangesAsync();

            return Ok(userTournament);
        }
    }
}