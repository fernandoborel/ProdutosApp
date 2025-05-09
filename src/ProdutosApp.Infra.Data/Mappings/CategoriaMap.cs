using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosApp.Domain.Entities;

namespace ProdutosApp.Infra.Data.Mappings;

/// <summary>
/// Classe para mapeamento da entidade Categoria no banco de dados
/// </summary>
public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c=> c.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(c => c.Nome)
            .IsUnique();
    }
}
