using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //primary keys
            modelBuilder.Entity<User>().HasKey(e => e.UserId);

            modelBuilder.Entity<Color>().HasKey(e => e.ColorId);

            modelBuilder.Entity<Position>().HasKey(e => e.PositionId);

            modelBuilder.Entity<Country>().HasKey(e => e.CountryId);


            //relations
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.HasOne(p => p.Position).WithMany(ps => ps.Players);

                entity.HasOne(p => p.Team).WithMany(t => t.Players);
            });


            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.Initials).HasColumnType("CHAR(3)").IsRequired();

                entity.HasOne(t => t.PrimaryKitColor).WithMany(pk => pk.HomeTeams).HasForeignKey(pk => pk.PrimaryKitColorId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.SecondaryKitColor).WithMany(sk => sk.AwayTeams).HasForeignKey(sk => sk.SecondaryKitColorId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Town).WithMany(t => t.Teams);
            });


            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.TownId);

                entity.HasOne(e => e.Country).WithMany(c => c.Towns);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.HasOne(g => g.HomeTeam).WithMany(ht => ht.HomeGames).HasForeignKey(ht => ht.HomeTeamId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.AwayTeam).WithMany(at => at.AwayGames).HasForeignKey(at => at.AwayTeamId).OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.GameId });

                //entity.HasOne(ps => ps.Game).WithMany(g => g.PlayerStatistics);

                //entity.HasOne(ps => ps.Player).WithMany(p => p.Statistics);
            });

            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.BetId);

                entity.HasOne(b => b.Game).WithMany(g => g.Bets);

                entity.HasOne(b => b.User).WithMany(u => u.Bets);
            });
        }
    }
}
