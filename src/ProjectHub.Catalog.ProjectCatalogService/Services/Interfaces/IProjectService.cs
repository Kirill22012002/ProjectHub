using ProjectHub.Catalog.ProjectCatalogService.ViewModels;

namespace ProjectHub.Catalog.ProjectCatalogService.Services.Interfaces;

public interface IProjectService
{
    Task CreateProjectAsync(NewProjectViewModel projectVm);
    Task<List<ProjectViewModel>> GetProjectsAsync();

}