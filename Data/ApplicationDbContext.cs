using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Models;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<PickedHistory> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PickedHistory>()
                .HasOne(h => h.Gifter)
                .WithMany()
                .HasForeignKey(h => h.GifterId);

            builder.Entity<PickedHistory>()
                .HasOne(h => h.Recipient)
                .WithMany()
                .HasForeignKey(h => h.RecipientId);
        }
    }
}