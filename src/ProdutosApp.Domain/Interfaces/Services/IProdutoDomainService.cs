using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Interfaces.Services;

public interface IProdutoDomainService
{
    Task Adicionar(Produto produto);
    Task Atualizar(Produto produto);
    Task<Produto> Inativar(Guid id);
    Task<List<Produto>> ObterTodos();
    Task<Produto> ObterPorId(Guid id);
}