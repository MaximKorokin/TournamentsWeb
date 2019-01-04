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
    public class TournamentsGamesController : Controller
    {
        private readonly TournamentsDbContext _context;

        public TournamentsGamesController(TournamentsDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournamentGame([FromRoute] int id)
        {
            var game = await _context.TournamentsGames
                .FindAsync(id);
            return Ok(game);
        }

        [HttpGet("u/{id}")]
        public IActionResult GetTournamentGameUsers([FromRoute] int id)
        {
            var users = _context.TournamentsGamesPlayers
                .Where(p => p.TournamentsGameId == id)
                .Include(p => p.User);
            return Ok(users);
        }

        [HttpGet("t/{id}")]
        public IActionResult GetTournamentGames([FromRoute] int id)
        {
            var games = _context.TournamentsGames
                .Where(tg => tg.TournamentId == id);
            return Ok(games);
        }

        [HttpGet]
        public async Task<IActionResult> GetUniqueGameId()
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                var game = await _context.TournamentsGames.FindAsync(i);
                if (game == null)
                    return Ok(i);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostTournamentGame([FromBody] TournamentsGame game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TournamentsGames.Add(game);
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        [HttpPost("u")]
        public async Task<IActionResult> PostTournamentGamePlayer([FromBody] TournamentsGamesPlayer player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TournamentsGamesPlayers.Add(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

        [HttpPut]
        public async Task<IActionResult> PutTournamentGame([FromBody] TournamentsGame game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("u/{id}")]
        public async Task<IActionResult> PutTournamentGamePlayer([FromRoute] int id, [FromBody] TournamentsGamesPlayer player)
        {
            var curPlayer = _context.TournamentsGamesPlayers.FirstOrDefault(p =>
                p.UserId == id && p.TournamentsGameId == player.TournamentsGameId);

            _context.TournamentsGamesPlayers.Remove(curPlayer);
            _context.TournamentsGamesPlayers.Add(player);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("t/{id}")]
        public async Task<IActionResult> DeleteTournamentGames([FromBody] int id)
        {
            foreach (var game in _context.TournamentsGames.Where(g => g.TournamentId == id))
                _context.TournamentsGames.Remove(game);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("u/{id}")]
        public async Task<IActionResult> DeleteTournamentGamePlayers([FromRoute] int id)
        {
            _context.TournamentsGamesPlayers.RemoveRange(
                _context.TournamentsGamesPlayers
                .Where(p => p.TournamentsGameId == id)
            );
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}