using FluentValidation;
using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Domain.Validations;

/// <summary>
/// Classe de regras de validação para Categoria com FluentValidation
/// </summary>
public class CategoriaValidator : AbstractValidator<Categoria>
{
    /// <summary>
    /// Método construtor contendo os mapeamentos das validações.
    /// </summary>
    public CategoriaValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("O Id da categoria não pode ser nulo.")
            .NotEqual(Guid.Empty).WithMessage("O Id da categoria não pode ser vazio.");

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome da categoria não pode ser vazio.")
            .Length(3, 50).WithMessage("O nome da categoria deve ter entre 3 e 50 caracteres.");
    }
}
