using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GighubApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set;}
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a=>a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);
          
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u=>u.Followers)
                .WithRequired(a => a.Followee)
                .WillCascadeOnDelete(false);
           
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followee)
                .WithRequired(a => a.Follower)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}