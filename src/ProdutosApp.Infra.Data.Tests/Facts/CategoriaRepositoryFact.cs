using Bogus;
using FluentAssertions;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Repositories;
using ProdutosApp.Infra.Data.Tests.Contexts;

namespace ProdutosApp.Infra.Data.Tests.Facts;

/// <summary>
/// Classe de execução de testes unitários para Categoria
/// </summary>
public class CategoriaRepositoryFact
{
    private readonly Faker<Categoria> _fakerCategoria;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaRepositoryFact()
    {
        _unitOfWork = new UnitOfWork(TestContext.CreateDataContext());

        _fakerCategoria = new Faker<Categoria>("pt_BR")
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Nome, f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.Ativo, true);
    }

    [Fact(DisplayName = "Adicionar categoria com sucesso no banco de dados.")]
    public async Task AdicionarCategoriaComSucesso()
    {
        var categoria = _fakerCategoria?.Generate();

        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        var registro = await _unitOfWork.CategoriaRepository.GetByIdAsync(categoria.Id);

        Assert.NotNull(registro);

        registro.Id.Should().Be(categoria.Id);
        registro.Nome.Should().Be(categoria.Nome);
    }

    [Fact(DisplayName = "Atualizar categoria com sucesso no banco de dados.")]
    public async Task AtualizarCategoriaComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        categoria.Nome = "Categoria Atualizada";
        await _unitOfWork.CategoriaRepository.UpdateAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        var atualizado = await _unitOfWork.CategoriaRepository.GetByIdAsync(categoria.Id);

        Assert.NotNull(atualizado);

        atualizado.Id.Should().Be(categoria.Id);
        atualizado.Nome.Should().Be("Categoria Atualizada");
    }

    [Fact(DisplayName = "Excluir categoria com sucesso no banco de dados.")]
    public async Task ExcluirCategoriaComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CategoriaRepository.DeleteAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        var excluido = await _unitOfWork.CategoriaRepository.GetByIdAsync(categoria.Id);

        Assert.Null(excluido);
    }

    [Fact(DisplayName = "Consultar categorias com sucesso no banco de dados.")]
    public async Task ConsultarCategoriasComSucesso()
    {
        for (int i = 0; i < 5; i++)
        {
            await _unitOfWork.CategoriaRepository.AddAsync(_fakerCategoria.Generate());
            await _unitOfWork.SaveChangesAsync();
        }

        var categorias = await _unitOfWork.CategoriaRepository.GetAllAsync();

        Assert.NotNull(categorias);

        categorias.Should().HaveCountGreaterThanOrEqualTo(5);
    }

    [Fact(DisplayName = "Obter categoria por Id com sucesso no banco de dados.")]
    public async Task ObterCategoriaPorIdComSucesso()
    {
        var categoria = _fakerCategoria.Generate();
        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        var resultado = await _unitOfWork.CategoriaRepository.GetByIdAsync(categoria.Id);

        Assert.NotNull(resultado);

        resultado.Id.Should().Be(categoria.Id);
        resultado.Nome.Should().Be(categoria.Nome);
    }
}