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
            #region " DbContext/UowData/Repositories "

            services.AddScoped<IUowData, UowData>();

            #endregion

            #region " Services "

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();
            #endregion
        }
    }
}
