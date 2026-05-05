using MediatR;

namespace Domain.Commands.v1.Models.Cancels;

public class CancelSaleCommand : IRequest
{
    public Guid Id { get; set; }
}