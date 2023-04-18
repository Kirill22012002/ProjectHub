using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
                
            },
            new List<string>()  
        }   
    });
});

builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("Bearer", options =>
    {
        
        // options.Authority = "http://localhost:43056/";
        options.Authority = "http://ec2-54-203-48-186.us-west-2.compute.amazonaws.com:5216";
        
        // options.Audience = "WebAPI";
        options.Audience = "TownSend_Backend_Resource";
        
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddHttpClient();

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