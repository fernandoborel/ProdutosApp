namespace ProdutosApp.Domain.Entities;

public class Categoria
{
    #region Propriedades

    public Guid? Id { get; set; }
    public string? Nome { get; set; }
    public bool? Ativo { get; set; }

    #endregion

    #region Relacionamentos

    public ICollection<Produto>? Produtos { get; set; }

    #endregion
}
