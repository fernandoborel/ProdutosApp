using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Contexts;

namespace ProdutosApp.Infra.Data.Repositories;

public class ProdutoRepository : BaseRepository<Produto, Guid>, IProdutoRepository
{
    public ProdutoRepository(DataContext dataContext) : base(dataContext)
    {
    }
}