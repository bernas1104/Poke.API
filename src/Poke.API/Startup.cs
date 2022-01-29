using System.Data.Common;
using System.Linq;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Npgsql;
using NSwag;
using NSwag.Generation.Processors.Security;
using Poke.API.Filters;
using Poke.API.AutoMapper;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Notifications;
using Poke.Infra.Context;
using Poke.Infra.Repositories;
using Poke.Infra.UoW;
using MediatR;

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
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddControllers();
            services.AddMvc(
                options => {
                    options.Filters.Add<DomainNotificationFilter>();
                    options.EnableEndpointRouting = false;
                }
            ).AddJsonOptions(
                options => options.JsonSerializerOptions.IgnoreNullValues = true
            );
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddMediatR(typeof(Startup));

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
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainNotification, DomainNotification>();

            if (!WebHostEnvironment.IsProduction())
            {
                services.AddOpenApiDocument(document =>
                {
                    document.DocumentName = "v1";
                    document.Version = "v1";
                    document.Title = "Poke API";
                    document.Description = "API de Pokemon";
                    document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                    document.AddSecurity(
                        "JWT",
                        Enumerable.Empty<string>(),
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = HeaderNames.Authorization,
                            Description = "Token de autenticação",
                            In = OpenApiSecurityApiKeyLocation.Header
                        }
                    );

                    document.PostProcess = (configure) =>
                    {
                        configure.Info.TermsOfService = "None";
                        configure.Info.Contact = new OpenApiContact()
                        {
                            Name = "Squad",
                            Email = "squad@xyz.com",
                            Url = "exemplo.xyz.com"
                        };
                        configure.Info.License = new OpenApiLicense()
                        {
                            Name = "Exemplo",
                            Url = "exemplo.xyz.com"
                        };
                    };
                });
            }
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

            if (!env.IsProduction())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
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
