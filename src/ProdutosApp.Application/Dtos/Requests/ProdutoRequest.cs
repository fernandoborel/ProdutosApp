namespace ProdutosApp.Application.Dtos.Requests;

/// <summary>
/// Modelo de dados da requisição da aplicação
/// para operações de cadastro e consulta de produtos
/// </summary>
public class ProdutoRequest
{
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public int? Quantidade { get; set; }
    public Guid CategoriaId { get; set; }
}