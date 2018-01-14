namespace JarvisEdge.IoC
{
    using JarvisEdge.Data.Repositories;
    using JarvisEdge.ServiceInterfaces;
    using JarvisEdge.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class JarvisEdgeContainer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUowData, UowData>();

            services.AddTransient<IUserService, UserService>();
        }
    }
}
