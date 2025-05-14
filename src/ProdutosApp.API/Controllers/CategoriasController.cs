using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Application.Dtos.Responses;
using ProdutosApp.Application.Interfaces;

namespace ProdutosApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController(ICategoriaAppService _categoriaAppService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoriaResponse>), 200)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _categoriaAppService.ObterTodos());
    }
}