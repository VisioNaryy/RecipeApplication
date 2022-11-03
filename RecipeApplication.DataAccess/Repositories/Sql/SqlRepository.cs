using Microsoft.EntityFrameworkCore;
using RecipeApplication.Core.Abstractions.Repositories;
using RecipeApplication.Core.Domain;

namespace RecipeApplication.DataAccess.Repositories.Sql;

public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    public SqlRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _context.Set<TEntity>().Where(x => !x.IsDeleted).ToListAsync();
        
        return entities;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesEntityExist(int id)
    {
        return await _context.Set<TEntity>().AnyAsync(x => !x.IsDeleted && x.Id == id);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await (_context.SaveChangesAsync());
    }
}