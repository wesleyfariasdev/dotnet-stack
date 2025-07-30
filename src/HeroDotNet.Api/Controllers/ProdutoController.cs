using HeroDotNet.Application.Commands.ProdutoCommand;
using HeroDotNet.Application.Queries.ProdutoQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeroDotNet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduto([FromBody] CreateProdutoCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid product data.");
        }
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProdutoByIdQuery), new { id = result.ToString() });

        //if (res)
        //{
        //    return CreatedAtAction(nameof(GetProdutoById), new { id = result.Value.Id }, result.Value);
        //}
        //return BadRequest(result.ErrorMessage);
    }
}