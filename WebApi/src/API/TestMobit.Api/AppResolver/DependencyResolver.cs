using Asp.Versioning;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TestMobit.Api.Abstraction;
using TestMobit.Api.Mapping;
using TestMobit.Api.Requirements;
using TestMobit.Api.Security;
using TestMobit.Domain.Configurations;
using TestMobit.Service.Hubs;

namespace TestMobit.Api.AppResolver
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddAuthentication();
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();

            services.RegisterHangfireDependencies(configuration);
            services.RegisterSwaggerDependencies(configuration);
            services.RegisterJwtBearerAuthorization(configuration);

            var cryptographySettings = configuration.GetSection("CryptographySettings").Get<CryptographySettings>() ?? 
                throw new InvalidOperationException("CryptographySettings section is missing or invalid in the configuration.");
            services.AddSingleton(cryptographySettings);

            var authenticationSettings = configuration.GetSection("AuthorizationSettings").Get<AuthorizationSettings>() ?? 
                throw new InvalidOperationException("AuthorizationSettings section is missing or invalid in the configuration.");
            services.AddSingleton(authenticationSettings);

            var jwtBearerToken = new JwtBearerToken(authenticationSettings);
            services.AddSingleton(jwtBearerToken);

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddLogging(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace));

            services.AddEndpoints();
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddHostedService<Worker.WorkerService.Worker>();

            return services;
        }

        public static IApplicationBuilder MapGroups(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            var services = app.Services.GetServices<IEndpointsApi>();

            IEndpointRouteBuilder endpoint = routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (IEndpointsApi service in services)
            {
                var group = endpoint.MapGroup($"/{service.GroupName}");
                group.WithTags(service.GroupName);
                service.MapEndpoints(group);
            }

            return app;
        }

        public static IApplicationBuilder MapHubs(this WebApplication app)
        {
            app.MapHub<EnterpriseHub>("/hub/enterprise");
            return app;
        }

        private static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            typeof(DependencyResolver).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointsApi).IsAssignableFrom(t))
                .ToList()
                .ForEach(t =>
                    t.GetInterfaces()
                        .Where(i => typeof(IEndpointsApi).IsAssignableFrom(i))
                        .ToList()
                        .ForEach(i => services.AddTransient(i, t))
                );

            return services;
        }

        private static IServiceCollection RegisterHangfireDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.UseInMemoryStorage();
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseDefaultTypeSerializer();
            });
            services.AddHangfireServer();

            return services;
        }

        private static IServiceCollection RegisterSwaggerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Monit API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                       {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                          },
                          new string[] { }
                        }
                  });
            });

            return services;
        }

        private static IServiceCollection RegisterJwtBearerAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AuthenticationSettings:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["AuthenticationSettings:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthenticationSettings:SecretKey"] ?? "aspentecnologiame@gmail.com")),

                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(
                    "Bearer",
                    new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build()
                );

                auth.AddPolicy("HasRole", police => police.Requirements.Add(new RoleRequirement()));

            });

            return services;
        }
    }
}
