using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<MatchSchedule> MatchSchedules { get; set; }
        public DbSet<GolfMatch> GolfMatches { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Weight> Weight { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent config, if any
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Golfer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Weight>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<GolfMatch>()
                .HasKey(gm => new {gm.GolferId, gm.MatchScheduleId});

            //I have one golfer that can have many matches
            //1 golfer can have many matches
            modelBuilder.Entity<GolfMatch>()
                .HasOne(gm => gm.Golfer)
                .WithMany(g => g.GolfMatches)
                .HasForeignKey(gm => gm.GolferId);

            //I have one match with many golfers
            //1 match will be between 2 golfers
            modelBuilder.Entity<GolfMatch>()
                .HasOne(gm => gm.MatchSchedule)
                .WithMany(m => m.GolfMatches)
                .HasForeignKey(gm => gm.MatchScheduleId);

            //I have many handicaps with one golfer
            //1 golfer has many handicaps (historically)
            modelBuilder.Entity<Golfer>()
                .HasMany(g => g.CourseHandicaps)
                .WithOne(h => h.Golfer)
                .HasForeignKey(g => g.HandicapId);

            modelBuilder.Entity<Golfer>()
                .HasMany(g => g.Scores)
                .WithOne(s => s.Golfer)
                .HasForeignKey(g => g.ScoreId);

            modelBuilder.Entity<Score>()
                .HasMany(s => s.HolesScore)
                .WithOne(h => h.Score);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.Course)
                .WithMany();

            modelBuilder.Entity<Golfer>()
                .HasOne(g => g.FlightLookup);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=my-temp-db.sqlite");
        }
    }
}
