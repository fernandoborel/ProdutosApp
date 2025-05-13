using ProdutosApp.Application.Dtos;
using ProdutosApp.Application.Interfaces;
using ProdutosApp.Domain.Interfaces.Services;

namespace ProdutosApp.Application.Services;

public class CategoriaAppService(ICategoriaDomainService _categoriaDomainService) : ICategoriaAppService
{
    public async Task<List<CategoriaResponse>> ObterTodos()
    {
        var categorias = await _categoriaDomainService.ObterTodos();

        return categorias.Select(c => new CategoriaResponse
        {
            Id = c.Id,
            Nome = c.Nome
        }).ToList();
    }
}