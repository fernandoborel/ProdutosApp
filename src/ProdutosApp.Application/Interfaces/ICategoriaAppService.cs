using ProdutosApp.Application.Dtos;

namespace ProdutosApp.Application.Interfaces;

public interface ICategoriaAppService
{
    Task<List<CategoriaResponse>> ObterTodos();
}