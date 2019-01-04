using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentsBack.Data;
using TournamentsBack.Models;

namespace Tournaments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentTypesController : ControllerBase
    {
        private readonly TournamentsDbContext _context;

        public TournamentTypesController(TournamentsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TournamentType> GetTournamentTypes()
        {
            return _context.TournamentTypes;
        }
    }
}