using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.Models;

namespace TournamentsBack.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public int TournamentTypeId { get; set; }
        public int DisciplineId { get; set; }
        public string Organizer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int MembersCapacity { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsFreeEntrance { get; set; }
        
        public Discipline Discipline { get; set; }
        public TournamentType TournamentType { get; set; }
    }
}
