using Azure.Identity;

namespace ProdutosApp.API.Extensions;

/// <summary>
/// Classe de extensão para acessarmos as informações de identificação e de cofres de chaves no Azure
/// </summary>
public static class AzureIdentityExtension
{
    public static IConfigurationBuilder AddAzureIdentity(this IConfigurationBuilder builder, IConfiguration configuration)
    {
        var keyVaultUrl = configuration["AzureKeyVault"];

        builder.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());

        return builder;
    }
}