using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace api.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<FavoriteJob> FavoriteJobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<JobCategory> Categories { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public new DbSet<User> Users { get; set; }

        public DbSet<JobRecommendation> JobRecommendations { get; set; }
        public DbSet<ApplicationDocument> ApplicationDocuments { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.CompanyId}));

            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Portfolios)
                .HasForeignKey(p => p.CompanyId);

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