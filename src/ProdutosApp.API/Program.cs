using ProdutosApp.API.Extensions;
using ProdutosApp.API.Middlewares;
using ProdutosApp.Application.Extensions;
using ProdutosApp.Domain.Extensions;
using ProdutosApp.Infra.Data.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Registrando os servi�os de inje��o de depend�ncia
builder.Services.AddAplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddEntityFramework(builder.Configuration);

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configura��es para Azure (Somente PRD)
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureIdentity(builder.Configuration);
}

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Scalar
app.MapScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.BluePlanet);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
