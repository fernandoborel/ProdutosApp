using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Interfaces.Services;

/// <summary>
/// Interface para operações de serviço de dominio de Categoria.
/// </summary>
public interface ICategoriaDomainService
{
    Task<List<Categoria>> ObterTodos();
}