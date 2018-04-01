namespace JarvisEdge.Data
{
    using JarvisEdge.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class JarvisDbContext : IdentityDbContext<ApplicationUser>
    {
        public JarvisDbContext(DbContextOptions<JarvisDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<OfferType> OfferTypes { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyStatus> PropertyStatuses { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Curency> Curencies { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<ApartamentType> ApartamentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<PropertyExtra>()
         .HasKey(pc => new { pc.PropertyId, pc.ExtraId });

            builder.Entity<PropertyExtra>()
                .HasOne(pc => pc.Property)
                .WithMany(p => p.Extras)
                .HasForeignKey(pc => pc.PropertyId);

            builder.Entity<PropertyExtra>()
                .HasOne(pc => pc.Extra)
                .WithMany(c => c.Properties)
                .HasForeignKey(pc => pc.ExtraId);
        }
    }
}
