using Microsoft.EntityFrameworkCore;
using ProjectHub.Catalog.ProjectCatalogService.Data.Models;
using Task = ProjectHub.Catalog.ProjectCatalogService.Data.Models.Task;

namespace ProjectHub.Catalog.ProjectCatalogService.Data;

public class ProjectCatalogDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public ProjectCatalogDbContext(DbContextOptions<ProjectCatalogDbContext> options) : base(options)
    {
    }
    
}