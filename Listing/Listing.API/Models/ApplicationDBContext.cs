using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Listing.API.Models
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .Property(f => f.RegistrationDate)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Entity<List>()
                .Property(f => f.RegistrationDate)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Entity<ListContent>()
                .Property(f => f.RegistrationDate)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Entity<Rating>()
                .Property(f => f.RegistrationDate)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Entity<Favorite>()
                .Property(f => f.RegistrationDate)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListContent> ListsContents { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
