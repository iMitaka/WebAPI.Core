namespace JarvisEdge.Data
{
    using JarvisEdge.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class JarvisDbConfiguration
    {
        public static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<JarvisDbContext>(options => options.UseSqlServer(JarvisDbConstants.GetConnectionString()));
        }

        public static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<JarvisDbContext>()
                    .AddDefaultTokenProviders();
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                JarvisDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<JarvisDbContext>();
                dbContext.Database.Migrate();

                // TODO: Use dbContext if you want to do seeding etc.
            }
        }
    }
}
