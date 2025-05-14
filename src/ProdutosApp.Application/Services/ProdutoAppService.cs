using ProdutosApp.Application.Dtos.Requests;
using ProdutosApp.Application.Dtos.Responses;
using ProdutosApp.Application.Interfaces;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Services;

namespace ProdutosApp.Application.Services;

/// <summary>
/// Implementação dos serviços de aplicação para produto
/// </summary>
public class ProdutoAppService(IProdutoDomainService produtoDomainService) : IProdutoAppService
{
    public async Task<ProdutoResponse> Adicionar(ProdutoRequest request)
    {
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Preco = request.Preco,
            Quantidade = request.Quantidade,
            DataHoraCriacao = DateTime.Now,
            CategoriaId = request.CategoriaId,
            Ativo = true
        };

        await produtoDomainService.Adicionar(produto);

        return Map(produto);
    }

    public async Task<ProdutoResponse> Atualizar(Guid id, ProdutoRequest request)
    {
        var produto = new Produto
        {
            Id = id,
            Nome = request.Nome,
            Preco = request.Preco,
            Quantidade = request.Quantidade,
            CategoriaId = request.CategoriaId
        };

        await produtoDomainService.Atualizar(produto);

        return Map(produto);
    }

    public async Task<ProdutoResponse> Excluir(Guid id)
    {
        var produto = await produtoDomainService.Inativar(id);

        return Map(produto);
    }

    public async Task<List<ProdutoResponse>> ObterTodos()
    {
        var produtos = await produtoDomainService.ObterTodos();

        var response = new List<ProdutoResponse>();
        foreach (var item in produtos)
            response.Add(Map(item));

        return response;
    }

    public async Task<ProdutoResponse?> ObterPorId(Guid id)
    {
        var produto = await produtoDomainService.ObterPorId(id);

        if (produto != null)
            return Map(produto);

        return null;
    }

    private ProdutoResponse Map(Produto produto)
    {
        return new ProdutoResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Quantidade = produto.Quantidade,
            DataHoraCriacao = produto.DataHoraCriacao,
            CategoriaId = produto.CategoriaId
        };
    }
}