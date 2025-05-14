using Microsoft.Extensions.DependencyInjection;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Services;

namespace ProdutosApp.Domain.Extensions;

public static class DomainServicesExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaDomainService, CategoriaDomainService>();
        services.AddScoped<IProdutoDomainService, ProdutoDomainService>();

        return services;
    }
}