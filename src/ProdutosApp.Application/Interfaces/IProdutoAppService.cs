using ProdutosApp.Application.Dtos.Requests;
using ProdutosApp.Application.Dtos.Responses;

namespace ProdutosApp.Application.Interfaces;

/// <summary>
/// Interface para serviços de aplicação de produto
/// </summary>
public interface IProdutoAppService
{
    Task<ProdutoResponse> Adicionar(ProdutoRequest request);
    Task<ProdutoResponse> Atualizar(Guid id, ProdutoRequest request);
    Task<ProdutoResponse> Excluir(Guid id);
    Task<List<ProdutoResponse>> ObterTodos();
    Task<ProdutoResponse> ObterPorId(Guid id);
}