using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Golfer> Golfer { get; set; }
        public DbSet<MatchSchedule> MatchSchedule { get; set; }
        public DbSet<GolfMatch> GolfMatch { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Weight> Weight { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Tee> Tee { get; set; }
        public DbSet<Major> Major { get; set; }
        public DbSet<GolfOpponent> GolfOpponent { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent config, if any
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Golfer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Weight>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<HoleScore>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Major>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<MatchSchedule>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Score>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Tee>().HasQueryFilter(x => !x.IsDeleted);

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

            modelBuilder.Entity<Golfer>()
                .HasMany(g => g.Scores)
                .WithOne(s => s.Golfer)
                .HasForeignKey(s => s.GolferId);

            modelBuilder.Entity<Golfer>()
                .HasOne(g => g.Flight);

            modelBuilder.Entity<HoleScore>()
                .HasOne(hs => hs.Score)
                .WithMany(s => s.HolesScore)
                .HasForeignKey(hs => hs.ScoreId)
                .IsRequired();

            //modelBuilder.Entity<Score>()
            //    .HasMany(s => s.HolesScore)
            //    .WithOne(h => h.Score);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.Tee)
                .WithMany()
                .HasForeignKey(s => s.TeeId);

            modelBuilder.Entity<MatchSchedule>()
                .HasOne(ms => ms.Major)
                .WithMany(m => m.MatchSchedules)
                .HasForeignKey(ms => ms.MajorId)
                .IsRequired(false); // allow null for regular weeks

            modelBuilder.Entity<Tee>()
                .HasKey(t => t.TeeId);

            modelBuilder.Entity<Tee>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<GolfOpponent>().HasNoKey();// no Primary key
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=my-temp-db.sqlite");
        }
    }
}
