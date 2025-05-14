using FluentValidation;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Exceptions;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Validations;

namespace ProdutosApp.Domain.Services;

/// <summary>
/// Implementação dos serviços de domínio de produto
/// </summary>
public class ProdutoDomainService(IUnitOfWork unitOfWork) : IProdutoDomainService
{
    public async Task Adicionar(Produto produto)
    {
        ValidarProduto(produto);

        await VerificarCategoria(produto.CategoriaId);

        await unitOfWork.ProdutoRepository.AddAsync(produto);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Atualizar(Produto produto)
    {
        var registro = await unitOfWork.ProdutoRepository.GetByIdAsync(produto.Id);
        if (registro == null)
            throw new NaoEncontradoException(nameof(Produto), produto.Id);

        ValidarProduto(produto);

        await VerificarCategoria(produto.CategoriaId);

        await unitOfWork.ProdutoRepository.UpdateAsync(produto);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Produto> Inativar(Guid id)
    {
        var produto = await unitOfWork.ProdutoRepository.GetByIdAsync(id);
        if (produto == null)
            throw new NaoEncontradoException(nameof(Produto), id);

        produto.Ativo = false;

        await unitOfWork.ProdutoRepository.UpdateAsync(produto);
        await unitOfWork.SaveChangesAsync();

        return produto;
    }

    public async Task<List<Produto>> ObterTodos()
    {
        return await unitOfWork.ProdutoRepository.GetAllAsync();
    }

    public async Task<Produto?> ObterPorId(Guid id)
    {
        return await unitOfWork.ProdutoRepository.GetByIdAsync(id);
    }

    private void ValidarProduto(Produto produto)
    {
        var validator = new ProdutoValidator();
        var result = validator.Validate(produto);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    private async Task VerificarCategoria(Guid categoriaId)
    {
        var categoria = await unitOfWork.CategoriaRepository.GetByIdAsync(categoriaId);
        if (categoria == null)
            throw new NaoEncontradoException(nameof(Categoria), categoriaId);
    }
}