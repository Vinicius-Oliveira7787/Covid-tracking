using Domain.Common;
using Domain.Countries;
using Domain.ApiConnection.Consumers;
using Infra;
using Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Domain.Authentication;
using FluentValidation.AspNetCore;
using WebApi.Controllers.CovidApi;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddScoped(typeof (IRepository<>), typeof (Repository<>));
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IConsumer, Consumer>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddDbContext<CovidContext>();

            services
                .AddControllers()
                .AddFluentValidation(fv => fv
                    .RegisterValidatorsFromAssemblyContaining<RequestValidator>()
                );


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("any",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    }
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var database = new CovidContext())
            {
                database.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseCors("any");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
