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
    public class UserTournamentsController : Controller
    {
        private readonly TournamentsDbContext _context;

        public UserTournamentsController(TournamentsDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IEnumerable<Tournament> GetUserTournaments([FromRoute]int id)
        {
            var tournaments = _context.UsersTournaments
                .Where(ut => ut.UserId == id)
                .Include(ut => ut.Tournament)
                .Select(ut => ut.Tournament);
            return tournaments;
        }
    }
}