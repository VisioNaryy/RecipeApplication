using RecipeApplication.Core.Domain;

namespace RecipeApplication.Core.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity: BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id);
    
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task AddAsync(TEntity entity);
    
    Task RemoveAsync(TEntity entity);

    Task<int> SaveChangesAsync();

    void SaveChanges();
}