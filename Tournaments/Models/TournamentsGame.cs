using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentsBack.Models
{
    public class TournamentsGame
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int WinnerId { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public Tournament Tournament { get; set; }
        [ForeignKey("Id, WinnerId")]
        public TournamentsGamesPlayer Winner { get; set; }
    }
}
