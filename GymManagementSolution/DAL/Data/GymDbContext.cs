using DAL.Models;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> optionsBuilder) : base(optionsBuilder)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = "Server= DESKTOP-BARKJTV\\MSSQLDEV22 ; Database= GymSystem ;Trusted_Connection=True ; TrustServerCertificate=True";
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
            modelBuilder.Entity<ApplicationUser>(ob =>
            {
                ob.Property(obj => obj.FirstName).HasColumnType("varchar(50)");
                ob.Property(obj => obj.LastName).HasColumnType("varchar(50)");
            });
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }

    }
}
