using HeroDotNet.Application.Commands.ProdutoCommand;
using HeroDotNet.Application.Queries.ProdutoQueries;
using HeroDotNet.Domain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeroDotNet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> CreateProduto([FromBody] CreateProdutoCommand command)
    {
        if (command == null)
            return BadRequest("Invalid product data.");

        var result = await mediator.Send(command);

        return CreatedAtAction(
            nameof(GetProdutoById),
            new { id = result.ToString() },
            result
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProdutoById(Guid id)
    {
        var produto = await mediator.Send(new GetProdutoByIdQuery(TbProdutoId.FromGuid(id)));
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }
}
