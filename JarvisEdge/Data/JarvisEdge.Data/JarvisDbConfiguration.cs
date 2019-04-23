namespace JarvisEdge.Data
{
    using JarvisEdge.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;

    public static class JarvisDbConfiguration
    {
        public static void AddDbContext(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<JarvisDbContext>(options => options.UseSqlite("Data Source=homes.db"));
        }

        public static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
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
