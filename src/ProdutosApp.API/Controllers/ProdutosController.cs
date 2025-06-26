using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Application.Dtos.Requests;
using ProdutosApp.Application.Dtos.Responses;
using ProdutosApp.Application.Interfaces;

namespace ProdutosApp.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProdutosController(IProdutoAppService produtoAppService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponse), 201)]
    public async Task<IActionResult> Post([FromBody] ProdutoRequest request)
    {
        return StatusCode(201, await produtoAppService.Adicionar(request));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProdutoResponse), 200)]
    public async Task<IActionResult> Put(Guid id, [FromBody] ProdutoRequest request)
    {
        return Ok(await produtoAppService.Atualizar(id, request));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProdutoResponse), 200)]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await produtoAppService.Excluir(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProdutoResponse>), 200)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await produtoAppService.ObterTodos());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProdutoResponse), 200)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await produtoAppService.ObterPorId(id);

        if (response != null)
            return Ok(response);
        else
            return NoContent();
    }
}