using Microsoft.EntityFrameworkCore.Storage;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Contexts;

namespace ProdutosApp.Infra.Data.Repositories;

public class UnitOfWork(DataContext _dataContext) : IUnitOfWork
{
    private IDbContextTransaction _transaction;

    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _transaction = _dataContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public ICategoriaRepository CategoriaRepository 
        => new CategoriaRepository(_dataContext);

    public IProdutoRepository ProdutoRepository 
        => new ProdutoRepository(_dataContext);

    public void Dispose()
    {
        _transaction?.Dispose();
        _dataContext?.Dispose();
    }
}