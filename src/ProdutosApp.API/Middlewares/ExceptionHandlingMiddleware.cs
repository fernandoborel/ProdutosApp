using FluentValidation;
using Newtonsoft.Json;
using ProdutosApp.Domain.Exceptions;
using System.Net;

namespace ProdutosApp.API.Middlewares;

/// <summary>
/// Middlware para tratamwento de exceções do projeto ASP.NET
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
       => _next = next;

    /// <summary>
    /// Método para inteceptar as requisições enviadas para o servidor e capturar as exceções provocadas pela requisição. 
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            await HandleValidationException(context, e);
        }
        catch (NaoEncontradoException e)
        {
            await HandleNaoEncontradoException(context, e);
        }
        catch (Exception e)
        {
            await HandleException(context, e);
        }
    }

    /// <summary>
    /// Método para fazer o tratamento dos erros do tipo ValidaionException.
    /// </summary>
    private static Task HandleValidationException(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errors = exception.Errors.Select(e => new
        {
            Name = e.PropertyName,
            Message = e.ErrorMessage,
            Severity = e.Severity.ToString()
        });

        var response = new
        {
            Message = "Ocorreram erros de validação.",
            Status = context.Response.StatusCode,
            Errors = errors
        };

        var jsonResponse = JsonConvert.SerializeObject(response);
        return context.Response.WriteAsync(jsonResponse);
    }

    /// <summary>
    ///  Método para fazer o tratamento dos erros do tipo NaoEncontradoException.
    /// </summary>
    private static Task HandleNaoEncontradoException(HttpContext context, NaoEncontradoException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            Message = "Registro não encontrado",
            Status = context.Response.StatusCode,
            Errors = exception.Message
        };

        var jsonResponse = JsonConvert.SerializeObject(response);
        return context.Response.WriteAsync(jsonResponse);
    }

    /// <summary>
    /// Método para fazer o tratamento dos erros do tipo Exception.
    /// </summary>
    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            Message = "Falha interna ao executar a operação.",
            Status = context.Response.StatusCode
        };

        var jsonResponse = JsonConvert.SerializeObject(response);
        return context.Response.WriteAsync(jsonResponse);
    }
}