using ProdutosApp.Application.Dtos.Responses;

namespace ProdutosApp.Application.Interfaces;

public interface ICategoriaAppService
{
    Task<List<CategoriaResponse>> ObterTodos();
}