﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentsBack.Data;

namespace Tournaments.Migrations
{
    [DbContext(typeof(TournamentsDbContext))]
    [Migration("20181221194841_tournamentUpdate1")]
    partial class tournamentUpdate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tournaments.Models.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("TournamentsBack.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("DisciplineId");

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsStarted");

                    b.Property<int>("MembersCapacity");

                    b.Property<string>("Name");

                    b.Property<string>("Organizer");

                    b.Property<int>("TournamentTypeId");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("TournamentTypeId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("TournamentsBack.Models.TournamentsGame", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Number");

                    b.Property<int>("TournamentId");

                    b.Property<int>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.HasIndex("Id", "WinnerId")
                        .IsUnique();

                    b.ToTable("TournamentsGames");
                });

            modelBuilder.Entity("TournamentsBack.Models.TournamentsGamesPlayer", b =>
                {
                    b.Property<int>("TournamentsGameId");

                    b.Property<int>("UserId");

                    b.HasKey("TournamentsGameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TournamentsGamesPlayers");
                });

            modelBuilder.Entity("TournamentsBack.Models.TournamentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TournamentTypes");
                });

            modelBuilder.Entity("TournamentsBack.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TournamentsBack.Models.UsersTournament", b =>
                {
                    b.Property<int>("TournamentId");

                    b.Property<int>("UserId");

                    b.HasKey("TournamentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersTournaments");
                });

            modelBuilder.Entity("TournamentsBack.Models.Tournament", b =>
                {
                    b.HasOne("Tournaments.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TournamentsBack.Models.TournamentType", "TournamentType")
                        .WithMany()
                        .HasForeignKey("TournamentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TournamentsBack.Models.TournamentsGame", b =>
                {
                    b.HasOne("TournamentsBack.Models.Tournament", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TournamentsBack.Models.TournamentsGamesPlayer", "Winner")
                        .WithOne("TournamentsGame")
                        .HasForeignKey("TournamentsBack.Models.TournamentsGame", "Id", "WinnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TournamentsBack.Models.TournamentsGamesPlayer", b =>
                {
                    b.HasOne("TournamentsBack.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TournamentsBack.Models.UsersTournament", b =>
                {
                    b.HasOne("TournamentsBack.Models.Tournament", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TournamentsBack.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
