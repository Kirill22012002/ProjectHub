using AutoMapper;
using ProjectHub.Catalog.ProjectCatalogService.Data.Models;
using ProjectHub.Catalog.ProjectCatalogService.ViewModels;

namespace ProjectHub.Catalog.ProjectCatalogService;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            // Project
            config.CreateMap<Project, NewProjectViewModel>().ReverseMap();
            config.CreateMap<Project, ProjectViewModel>().ReverseMap();
        });
    }
}