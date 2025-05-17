using Bogus;
using FluentAssertions;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Repositories;
using ProdutosApp.Infra.Data.Tests.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ProdutosApp.Infra.Data.Tests.Facts;

/// <summary>
/// Classe de execução de testes unitários para Produto
/// </summary>
public class ProdutoRepositoryFact
{
    private readonly Faker<Categoria> _fakerCategoria;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Faker<Produto> _fakerProduto;

    public ProdutoRepositoryFact()
    {
        _unitOfWork = new UnitOfWork(TestContext.CreateDataContext());

        _fakerCategoria = new Faker<Categoria>("pt_BR")
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Nome, f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.Ativo, true);

        _fakerProduto = new Faker<Produto>("pt_BR")
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
            .RuleFor(p => p.Preco, f => f.Random.Decimal(10, 1000))
            .RuleFor(p => p.Quantidade, f => f.Random.Int(1, 100))
            .RuleFor(p => p.DataHoraCriacao, f => DateTime.Now)
            .RuleFor(p => p.Ativo, true);
    }

    [Fact(DisplayName = "Adicionar categoria com sucesso no banco de dados.")]
    public async Task AdicionarProdutoComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        var produto = _fakerProduto.Generate();
        produto.CategoriaId = categoria.Id;

        await _unitOfWork.ProdutoRepository.AddAsync(produto);
        await _unitOfWork.SaveChangesAsync();

        var registro = await _unitOfWork.ProdutoRepository.GetByIdAsync(produto.Id);

        Assert.NotNull(registro);
        registro.Nome.Should().Be(produto.Nome);
        registro.Preco.Should().Be(produto.Preco);
        registro.Quantidade.Should().Be(produto.Quantidade);
        registro.CategoriaId.Should().Be(categoria.Id);
    }

    [Fact(DisplayName = "Atualizar produto com sucesso no banco de dados.")]
    public async Task AtualizarProdutoComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();
        
        var produto = _fakerProduto.Generate();
        produto.CategoriaId = categoria.Id;
        
        await _unitOfWork.ProdutoRepository.AddAsync(produto);
        await _unitOfWork.SaveChangesAsync();
        
        produto.Nome = "Produto Atualizado";
        produto.Preco = 999.99m;
        produto.Quantidade = 10;
        
        await _unitOfWork.ProdutoRepository.UpdateAsync(produto);
        await _unitOfWork.SaveChangesAsync();
        
        var atualizado = await _unitOfWork.ProdutoRepository.GetByIdAsync(produto.Id);

        Assert.NotNull(atualizado);
        atualizado.Nome.Should().Be("Produto Atualizado");
        atualizado.Preco.Should().Be(999.99m);
        atualizado.Quantidade.Should().Be(10);
    }

    [Fact(DisplayName = "Excluir produto com sucesso no banco de dados.")]
    public async Task ExcluirProdutoComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();
        
        var produto = _fakerProduto.Generate();
        produto.CategoriaId = categoria.Id;
        
        await _unitOfWork.ProdutoRepository.AddAsync(produto);
        await _unitOfWork.SaveChangesAsync();
        
        await _unitOfWork.ProdutoRepository.DeleteAsync(produto);
        await _unitOfWork.SaveChangesAsync();
        
        var excluido = await _unitOfWork.ProdutoRepository.GetByIdAsync(produto.Id);

        Assert.Null(excluido);
    }

    [Fact(DisplayName = "Consultar produtos com sucesso no banco de dados.")]
    public async Task ConsultarProdutosComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();
        
        for (int i = 0; i < 5; i++)
        {
            var produto = _fakerProduto.Generate();
            produto.CategoriaId = categoria.Id;
            
            await _unitOfWork.ProdutoRepository.AddAsync(produto);
            await _unitOfWork.SaveChangesAsync();
        }
        
        var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();

        Assert.NotNull(produtos);
        produtos.Should().HaveCountGreaterThanOrEqualTo(5);
    }

    [Fact(DisplayName = "Obter produto por Id com sucesso no banco de dados.")]
    public async Task ObterProdutoPorIdComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();
        
        var produto = _fakerProduto.Generate();
        produto.CategoriaId = categoria.Id;
        
        await _unitOfWork.ProdutoRepository.AddAsync(produto);
        await _unitOfWork.SaveChangesAsync();
        
        var resultado = await _unitOfWork.ProdutoRepository.GetByIdAsync(produto.Id);

        Assert.NotNull(resultado);
        resultado!.Id.Should().Be(produto.Id);
        resultado.Nome.Should().Be(produto.Nome);
        resultado.Preco.Should().Be(produto.Preco);
        resultado.Quantidade.Should().Be(produto.Quantidade);
    }
}