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

        public DbSet<Kota> kotas { get; set; }

        public DbSet<Provinsi> provinsis { get; set; }

        public DbSet<ApiIGModel> apiIGModels { get; set; }
/*        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:dicerdb.database.windows.net,1433;Initial Catalog=DiCerDb;Persist Security Info=False;User ID=dicerhost;Password=d1cerhost@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }*/
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
