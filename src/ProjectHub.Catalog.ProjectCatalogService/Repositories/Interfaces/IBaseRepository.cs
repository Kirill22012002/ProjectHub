using ProjectHub.Catalog.ProjectCatalogService.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHub.Catalog.ProjectCatalogService.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseModel
{
    bool Any();

    Task<T> GetByIdAsync(long id);

    Task<List<T>> GetAllAsync();

    void Add(T model);

    Task AddAsync(T model);

    Task<bool> TryAddAsync(T model);

    void AddList(List<T> models);

    Task AddListAsync(List<T> models);

    Task UpdateAsync(T model);

    Task DeleteAsync(T model);

    Task DeleteByIdAsync(long id);

}