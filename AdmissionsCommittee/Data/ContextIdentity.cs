using AdmissionsCommittee.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdmissionsCommittee.Data
{
    public class ContextIdentity:IdentityDbContext<User>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<UserDocumentFile> UserDocumentFiles { get; set; }
        public DbSet<NameCourse> NameCourses { get; set; }

        public ContextIdentity(DbContextOptions<ContextIdentity> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(b => b.UserDocument)
               .WithOne(i => i.User)
                .HasForeignKey<UserDocument>(b => b.UserId);

            modelBuilder.Entity<User>()
               .HasOne(b => b.UserDocumentFile)
                .WithOne(i => i.User)
                .HasForeignKey<UserDocumentFile>(b => b.UserId);

           modelBuilder.Entity<User>()
                .HasOne(b => b.UserProfile)
                .WithOne(i => i.User)
                .HasForeignKey<UserProfile>(b => b.UserId);
        }
    }
}
