namespace ProjectHub.Catalog.ProjectCatalogService.Data.Models;

public class Project : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public List<Task> Tasks { get; set; }
}