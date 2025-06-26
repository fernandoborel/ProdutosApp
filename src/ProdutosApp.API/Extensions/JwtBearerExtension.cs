using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProdutosApp.API.Extensions;

public static class JwtBearerExtension
{
    public static IServiceCollection AddJwtBearerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //lendo as configs do appsettings.json
        var jwtSettings = new JwtSettings();
        new ConfigureFromConfigurationOptions<JwtSettings>
            (configuration.GetSection("JwtSettings")).Configure(jwtSettings);

        //injeção de dependência
        services.AddSingleton(jwtSettings);

        //politica de autenticação
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true, //emissor do token
                    ValidateAudience = true, //destinatário do token
                    ValidateLifetime = true, //validade do token
                    ValidateIssuerSigningKey = true, //chave de assinatura do token
                    ValidIssuer = jwtSettings.Issuer, //comparando o emissor do token
                    ValidAudience = jwtSettings.Audience, //comparando o destinatário do token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey ?? string.Empty)) //chave de assinatura do token
                };
            });

        return services;
    }
}

public class JwtSettings
{
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}