namespace ProdutosApp.Domain.Exceptions;

/// <summary>
/// Classe de exceção customizada para erros de valores não encontrados
/// </summary>
public class NaoEncontradoException : Exception
{
    public NaoEncontradoException(string entidade, Guid? id)
        : base($"{entidade} com identificador '{id}' não foi encontrado.")
    {

    }

    public NaoEncontradoException(string mensagem)
        : base(mensagem)
    {

    }
}