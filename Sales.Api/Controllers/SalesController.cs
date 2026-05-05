using Domain.Commands.v1.Models.Cancels;
using Domain.Commands.v1.Models.Creates;
using Domain.Queries.v1.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sales.Api.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController(
    IMediator mediator
) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
        => Ok(await _mediator.Send(new GetSaleByIdQuery { Id = id }));

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _mediator.Send(new CancelSaleCommand { Id = id });
        return NoContent();
    }
}