using Microsoft.EntityFrameworkCore;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Contexts;

namespace ProdutosApp.Infra.Data.Repositories;

public abstract class BaseRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey> where TEntity : class
{
    protected readonly DataContext _dataContext;

    protected BaseRepository(DataContext dataContext)
      =>  _dataContext = dataContext;

    public virtual async Task AddAsync(TEntity obj)
    {
        await _dataContext.Set<TEntity>().AddAsync(obj);
    }

    public virtual async Task UpdateAsync(TEntity obj)
    {
        await Task.FromResult(_dataContext.Set<TEntity>().Update(obj));
    }

    public virtual async Task DeleteAsync(TEntity obj)
    {
        await Task.FromResult(_dataContext.Set<TEntity>().Remove(obj));
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dataContext.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Tkey id)
    {
        return await _dataContext.Set<TEntity>().FindAsync(id);
    }
}