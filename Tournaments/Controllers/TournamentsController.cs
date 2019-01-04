using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentsBack.Data;
using TournamentsBack.Models;

namespace Tournaments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentsDbContext _context;

        public TournamentsController(TournamentsDbContext context)
        {
            _context = context;
        }

        // GET: api/Tournaments
        [HttpGet]
        public IEnumerable<Tournament> GetTournaments()
        {
            var t1 = _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.TournamentType);
            return t1;
        }

        [HttpGet("s/{name}")]
        public IEnumerable<Tournament> GetSearchedTournaments([FromRoute] string name)
        {
            var t1 = _context.Tournaments
                .Where(t => t.Name.IndexOf(name) > -1)
                .Include(t => t.Discipline)
                .Include(t => t.TournamentType);
            return t1;
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public IActionResult GetTournament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament = _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.TournamentType)
                .FirstOrDefault(t => t.Id == id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        [HttpPut("s/{id}")]
        public IActionResult StartTournament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament = _context.Tournaments
                .FirstOrDefault(t => t.Id == id);

            if (tournament == null)
            {
                return NotFound();
            }

            tournament.IsStarted = true;
            _context.SaveChanges();

            return Ok();
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament([FromRoute] int id, [FromBody] Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tournament.Id)
            {
                return BadRequest();
            }

            if (tournament.MembersCapacity < 2)
                tournament.MembersCapacity = 2;
            else if (tournament.MembersCapacity > 100)
                tournament.MembersCapacity = 100;

            _context.Entry(tournament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<IActionResult> PostTournament([FromBody] Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tournament.MembersCapacity < 2)
                tournament.MembersCapacity = 2;
            else if (tournament.MembersCapacity > 100)
                tournament.MembersCapacity = 100;

            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return Ok(tournament);
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}