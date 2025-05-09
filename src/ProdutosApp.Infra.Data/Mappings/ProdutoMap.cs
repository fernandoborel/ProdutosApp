using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Infra.Data.Mappings;

/// <summary>
/// Classe para mapeamento da entidade Produto no banco de dados
/// </summary>
public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id); //chave primária
        
        builder.Property(p => p.Nome)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.Preco)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(p => p.Quantidade)
            .IsRequired();
        
        builder.Property(p => p.DataHoraCriacao)
            .HasColumnType("datetime")
            .IsRequired();
        
        builder.Property(p => p.Ativo)
            .IsRequired();
        
        builder.Property(p => p.CategoriaId)
            .IsRequired();
        
        builder.HasOne(p => p.Categoria) //Produto TEM 1 Categoria
            .WithMany(c => c.Produtos) //Categoria TEM muitos Produtos
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
