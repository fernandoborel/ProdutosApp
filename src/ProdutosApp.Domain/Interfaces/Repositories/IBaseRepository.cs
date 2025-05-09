namespace ProdutosApp.Domain.Interfaces.Repositories;

/// <summary>
/// Interface para repositório genérico.
/// </summary>
public interface IBaseRepository<TEntity, Tkey> where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task UpdateAsync(TEntity obj);
    Task DeleteAsync(Tkey obj);

    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Tkey id);
}