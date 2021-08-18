using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Poke.Application.Services;
using Poke.Application.Services.Interfaces;
using Poke.Core.Interfaces.Repositories;
using Poke.Infra.Context;
using Poke.Infra.Repositories;

namespace Poke.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment
        )
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            var healthCheck = services.AddHealthChecksUI(
                setupSettings: setup =>
                {
                    setup.DisableDatabaseMigrations();
                    setup.MaximumHistoryEntriesPerEndpoint(6);
                }
            ).AddInMemoryStorage();

            var builder = services.AddHealthChecks();

            builder.AddProcessAllocatedMemoryHealthCheck(
                500 * 1024 * 1024,
                "Process Memory",
                tags: new[] { "self" }
            );

            builder.AddPrivateMemoryHealthCheck(
                500 * 1024 * 1024,
                "Private memory",
                tags: new[] { "self" }
            );

            builder.AddNpgSql(
                Configuration["ConnectionStrings:PokemonDB"],
                tags: new[] { "services" }
            );

            services.AddDbContext<EntityContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("PokemonDB"),
                    b => b.MigrationsAssembly("Poke.Infra")
                )
            );

            services.AddSingleton<DbConnection>(
                conn => new NpgsqlConnection(
                    Configuration.GetConnectionString("PokemonDB")
                )
            );

            services.AddScoped<DapperContext>();

            services.AddScoped<IPokemonsService, PokemonsService>();

            services.AddScoped<IPokemonsRepository, PokemonsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks(
                "/health",
                new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    Predicate = r => r.Tags.Contains("self"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );

            app.UseHealthChecks(
                "/ready",
                new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    Predicate = r => r.Tags.Contains("services"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
