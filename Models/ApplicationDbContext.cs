using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dicer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kota> Kota { get; set; }

        public DbSet<Provinsi> Provinsi { get; set; }

        public DbSet<ApiIGModel> apiIGModels { get; set; }

        public DbSet<Campaign> Campaign { get; set; }

        public DbSet<ClientCampaign> ClientCampaign { get; set; }

        public DbSet<CreatorJob> CreatorJob { get; set; }

        public DbSet<AcceptanceUser> AcceptanceUser { get; set; }

        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AcceptanceUser>().HasNoKey();
        }
    }

    internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(128);
            builder.Property(u => u.Gender).HasMaxLength(128);
        }
    }
}
