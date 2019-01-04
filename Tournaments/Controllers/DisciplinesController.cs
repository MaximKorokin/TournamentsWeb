using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Models;
using TournamentsBack.Data;

namespace Tournaments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinesController : Controller
    {
        private readonly TournamentsDbContext _context;

        public DisciplinesController(TournamentsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Discipline> GetTournamentTypes()
        {
            return _context.Disciplines;
        }
    }
}