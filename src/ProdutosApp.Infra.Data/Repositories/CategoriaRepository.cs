using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Contexts;

namespace ProdutosApp.Infra.Data.Repositories;

public class CategoriaRepository : BaseRepository<Categoria, Guid>, ICategoriaRepository
{
    public CategoriaRepository(DataContext dataContext) : base(dataContext)
    {
    }
}