using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Interfaces.Repositories;

/// <summary>
/// Interface para repositório de categorias.
/// </summary>
public interface ICategoriaRepository : IBaseRepository<Categoria, Guid>
{
}