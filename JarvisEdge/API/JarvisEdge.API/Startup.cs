namespace JarvisEdge.API
{
    using JarvisEdge.Data;
    using JarvisEdge.Helpers.Jwt;
    using JarvisEdge.IoC;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Swashbuckle.AspNetCore.Swagger;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Threading.Tasks;

    public class Startup
    {
        private const string apiTitle = "Jarvis API";
        private const string apiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            ConfigureDbContext(services);
            ConfigureIdentity(services);
            ConfigureAuthentication(services);
            JarvisEdgeContainer.ConfigureServices(services);
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiVersion, new Info { Title = apiTitle, Version = apiVersion });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.UseAuthentication();
            DatabaseInitializer(app);
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiTitle);
            });
        }

        private void CreateServerFolder(IHostingEnvironment env, string name)
        {
            var directoryPath = env.ContentRootPath + $"\\{name}";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            JarvisDbConfiguration.AddDbContext(services, Configuration);
        }

        private void DatabaseInitializer(IApplicationBuilder app)
        {
            JarvisDbConfiguration.InitializeDatabase(app);
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            JarvisDbConfiguration.AddIdentity(services);
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication((options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }))
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters =
                            new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,

                                ValidIssuer = Helpers.Jwt.JwtConstants.GetIssuer(),
                                ValidAudience = Helpers.Jwt.JwtConstants.GetAudience(),
                                IssuerSigningKey =
                                 JwtSecurityKey.Create(Helpers.Jwt.JwtConstants.GetSigningKey())
                            };

                       options.Events = new JwtBearerEvents
                       {
                           OnAuthenticationFailed = context =>
                           {
                               return Task.CompletedTask;
                           },
                           OnTokenValidated = context =>
                           {
                               return Task.CompletedTask;
                           },
                           OnMessageReceived = context =>
                           {
                               var signalRTokenHeader = context.Request.Query["signalRTokenHeader"];

                               if (!string.IsNullOrEmpty(signalRTokenHeader) &&
                                   (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                               {
                                   context.Token = context.Request.Query["signalRTokenHeader"];
                               }
                               return Task.CompletedTask;
                           }
                       };
                   });
        }
    }
}
