namespace ProdutosApp.Domain.Entities;

public class Produto
{
    #region Propriedades
    
    public Guid? Id { get; set; }
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public int? Quantidade { get; set; }
    public DateTime? DataHoraCriacao { get; set; }
    public bool? Ativo { get; set; }
    public Guid? CategoriaId { get; set; }
    
    #endregion
    
    #region Relacionamentos
    
    public Categoria? Categoria { get; set; }
    
    #endregion
}
