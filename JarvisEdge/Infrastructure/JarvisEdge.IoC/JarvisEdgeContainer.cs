namespace JarvisEdge.IoC
{
    using JarvisEdge.ServiceInterfaces;
    using JarvisEdge.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class JarvisEdgeContainer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
