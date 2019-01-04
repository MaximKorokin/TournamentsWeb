using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentsBack.Models
{
    public class TournamentsGamesPlayer
    {
        [Key, Column(Order = 0)]
        public int TournamentsGameId { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public TournamentsGame TournamentsGame { get; set; }
        public TournamentsGamesPlayer User { get; set; }
    }
}
