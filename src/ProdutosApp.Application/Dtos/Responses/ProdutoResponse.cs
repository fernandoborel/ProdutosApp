namespace ProdutosApp.Application.Dtos.Responses;

/// <summary>
/// Modelo de dados da resposta da aplicação
/// para uma operação de produto
/// </summary>
public class ProdutoResponse
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public int? Quantidade { get; set; }
    public DateTime? DataHoraCriacao { get; set; }
    public Guid CategoriaId { get; set; }
}