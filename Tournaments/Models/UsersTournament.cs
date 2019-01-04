using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentsBack.Models
{
    public class UsersTournament
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int TournamentId { get; set; }

        public User User { get; set; }
        public Tournament Tournament { get; set; }
    }
}
