using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movies.API.Data;
using Movies.API.Services;

namespace Movies.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MoviesContext>(options =>
                    options.UseInMemoryDatabase(("MoviesAPIContext")));

            services.AddAuthentication("Bearer")
                .AddJwtBearer(opt =>
                {
                    opt.Authority = "https://localhost:5005";
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movie_api_client", "movies_mvc_client"));
            });
            
            services.AddHttpContextAccessor();

            AddSwagger(services);

            services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(builder =>
           builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Api");
                    opt.RoutePrefix = string.Empty;
                });
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Movie - WebApi",
                        Description = "This Api will be responsible for managing the movies.",
                        Contact = new OpenApiContact
                        {
                            Name = "HeliosCreation",
                            Email = "reliableDevelopment@hotmail.com",
                        }
                    });
            });

        }
    }
}
