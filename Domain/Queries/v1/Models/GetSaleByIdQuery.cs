using Domain.Entities.v1;
using MediatR;

namespace Domain.Queries.v1.Models;

public class GetSaleByIdQuery : IRequest<Sale>
{
    public Guid Id { get; set; }
}