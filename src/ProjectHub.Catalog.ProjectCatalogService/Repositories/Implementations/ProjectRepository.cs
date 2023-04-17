using ProjectHub.Catalog.ProjectCatalogService.Data;
using ProjectHub.Catalog.ProjectCatalogService.Data.Models;
using ProjectHub.Catalog.ProjectCatalogService.Repositories.Interfaces;

namespace ProjectHub.Catalog.ProjectCatalogService.Repositories.Implementations;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ProjectCatalogDbContext context) : base(context)
    {
    }
}