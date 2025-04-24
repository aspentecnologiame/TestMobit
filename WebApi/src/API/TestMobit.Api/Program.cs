using Asp.Versioning;
using Asp.Versioning.Builder;
using Hangfire;
using TestMobit.Api.AppResolver;
using TestMobit.Infra.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

ConfigurationManager configuration = builder.Configuration;
builder.Services.RegisterAppDependencies(configuration);
builder.Services.RegisterCrossCuttingDependencies(configuration);

var app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapGroups(versionedGroup);
app.MapHubs();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseCors("CorsPolicy");
app.UseHangfireDashboard();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
