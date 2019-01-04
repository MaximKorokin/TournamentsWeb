using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.Models;
using TournamentsBack.Models;

namespace TournamentsBack.Data
{
    public class TournamentsDbContext : DbContext
    {
        public TournamentsDbContext(DbContextOptions<TournamentsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentType> TournamentTypes { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<TournamentsGame> TournamentsGames { get; set; }
        public DbSet<TournamentsGamesPlayer> TournamentsGamesPlayers { get; set; }
        public DbSet<UsersTournament> UsersTournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TournamentsGamesPlayer>().HasKey(player => new { player.TournamentsGameId, player.UserId });
            modelBuilder.Entity<UsersTournament>().HasKey(tournament => new { tournament.TournamentId, tournament.UserId });
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
