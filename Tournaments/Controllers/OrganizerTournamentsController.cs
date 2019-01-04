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
    public class OrganizerTournamentsController : Controller
    {
        private readonly TournamentsDbContext _context;

        public OrganizerTournamentsController(TournamentsDbContext context)
        {
            _context = context;
        }

        [HttpGet("{name}")]
        public IEnumerable<Tournament> GetOrganizerTournaments([FromRoute]string name)
        {
            var tournaments = _context.Tournaments
                .Where(t => t.Organizer == name)
                .Include(t => t.TournamentType);
            return tournaments;
        }
    }
}