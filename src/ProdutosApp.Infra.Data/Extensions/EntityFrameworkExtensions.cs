using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Data.Contexts;
using ProdutosApp.Infra.Data.Repositories;

namespace ProdutosApp.Infra.Data.Extensions;

/// <summary>
/// Classe de extensão para registrar o entity framework no container de injeção de dependência.
/// </summary>
public static class EntityFrameworkExtensions
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        // Adiciona o DbContext com a string de conexão do banco de dados
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ProdutosAppBD")));

        //injeção de dependencia para o UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}