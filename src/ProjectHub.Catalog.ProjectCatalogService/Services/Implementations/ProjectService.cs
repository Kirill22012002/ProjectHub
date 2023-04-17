using AutoMapper;
using ProjectHub.Catalog.ProjectCatalogService.Data.Models;
using ProjectHub.Catalog.ProjectCatalogService.Repositories.Interfaces;
using ProjectHub.Catalog.ProjectCatalogService.Services.Interfaces;
using ProjectHub.Catalog.ProjectCatalogService.ViewModels;
using Task = System.Threading.Tasks.Task;

namespace ProjectHub.Catalog.ProjectCatalogService.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(
        IProjectRepository projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task CreateProjectAsync(NewProjectViewModel projectVm)
    {
        var projectDb = _mapper.Map<Project>(projectVm);
        await _projectRepository.AddAsync(projectDb);
    }

    public async Task<List<ProjectViewModel>> GetProjectsAsync()
    {
        var projectsDb = await _projectRepository.GetAllAsync();
        var projectsVm = _mapper.Map<List<ProjectViewModel>>(projectsDb);
        return projectsVm;
    }
}