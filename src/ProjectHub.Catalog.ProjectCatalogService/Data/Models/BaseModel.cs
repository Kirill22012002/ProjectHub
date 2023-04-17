namespace ProjectHub.Catalog.ProjectCatalogService.Data.Models;

public abstract class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
}