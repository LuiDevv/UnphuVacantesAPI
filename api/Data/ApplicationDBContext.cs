using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UNPHU_Vacantes.Models;
using api.Models;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; } // Agrega esta línea
        public DbSet<FavoriteJob> FavoriteJobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<JobCategory> Categories { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbSet<Vacant> Vacants { get; set; }
        public DbSet<JobRecommendation> JobRecommendations { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
         public DbSet<SavedVacant> SavedVacants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.CompanyId }));

            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Portfolios)
                .HasForeignKey(p => p.CompanyId);

            // Configurar la relación entre Vacant y Application
            builder.Entity<Application>()
                .HasOne(ja => ja.Vacant)
                .WithMany(v => v.Applications)
                .HasForeignKey(ja => ja.VacantId);

            // Configurar la relación entre AppUser y Application
            builder.Entity<Application>()
                .HasOne(ja => ja.AppUser)
                .WithMany(u => u.Applications)
                .HasForeignKey(ja => ja.AppUserId);

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Employer", NormalizedName = "EMPLOYER" },
                new IdentityRole { Name = "JobSeeker", NormalizedName = "JOBSEEKER" }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-81N9E2R\\SQLEXPRESS;Initial Catalog=UnphuVacantes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
