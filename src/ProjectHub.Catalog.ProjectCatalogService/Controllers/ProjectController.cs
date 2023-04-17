using Microsoft.AspNetCore.Mvc;
using ProjectHub.Catalog.ProjectCatalogService.Repositories.Interfaces;
using ProjectHub.Catalog.ProjectCatalogService.Services.Interfaces;
using ProjectHub.Catalog.ProjectCatalogService.ViewModels;

namespace ProjectHub.Catalog.ProjectCatalogService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    
    public ProjectController(
        IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody]NewProjectViewModel projectVm)
    {
        await _projectService.CreateProjectAsync(projectVm);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projectsVm = await _projectService.GetProjectsAsync();
        return Ok(projectsVm);
    }
}