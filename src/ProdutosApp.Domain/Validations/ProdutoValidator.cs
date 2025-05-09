using FluentValidation;
using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Validations;

/// <summary>
/// Classe de regras de para Produto com FluentValidation.
/// </summary>
public class ProdutoValidator : AbstractValidator<Produto>
{
    /// <summary>
    /// Método construtor contendo os mapeamentos das validações.
    /// </summary>
    public ProdutoValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage("O Id do produto não pode ser nulo.")
            .NotEqual(Guid.Empty).WithMessage("O Id do produto não pode ser vazio.");

        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome do produto não pode ser vazio.")
            .Length(3, 150).WithMessage("O nome do produto deve ter entre 3 e 150 caracteres.");

        RuleFor(p => p.Preco)
            .NotNull().WithMessage("O preço do produto não pode ser nulo.")
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");

        RuleFor(p => p.Quantidade)
            .NotNull().WithMessage("A quantidade do produto não pode ser nula.")
            .GreaterThan(0).WithMessage("A quantidade do produto deve ser maior que zero.");

        RuleFor(p => p.DataHoraCriacao)
            .NotNull().WithMessage("A data de criação do produto não pode ser nula.");

        RuleFor(p => p.Ativo)
            .NotNull().WithMessage("O status do produto não pode ser nulo.");

        RuleFor(p => p.CategoriaId)
            .NotNull().WithMessage("O Id da categoria do produto não pode ser nulo.")
            .NotEqual(Guid.Empty).WithMessage("O Id da categoria do produto não pode ser vazio.");
    }
}
