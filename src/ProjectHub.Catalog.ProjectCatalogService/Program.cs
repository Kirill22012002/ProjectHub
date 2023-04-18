using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProjectHub.Catalog.ProjectCatalogService;
using ProjectHub.Catalog.ProjectCatalogService.Data;
using ProjectHub.Catalog.ProjectCatalogService.Repositories.Implementations;
using ProjectHub.Catalog.ProjectCatalogService.Repositories.Interfaces;
using ProjectHub.Catalog.ProjectCatalogService.Services.Implementations;
using ProjectHub.Catalog.ProjectCatalogService.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:43056/";
        options.Audience = "WebAPI";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddDbContext<ProjectCatalogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DockerLocalConnString")));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IProjectService, ProjectService>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ProjectCatalogDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();