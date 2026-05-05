using Domain.Entities.v1;
using Domain.Interfaces.v1.Repositories;
using Domain.Queries.v1.Models;
using MediatR;

namespace Domain.Queries.v1;

public class GetSaleByIdQueryHandler(
    ISaleRepository repository
) : IRequestHandler<GetSaleByIdQuery, Sale>
{
    private readonly ISaleRepository _repository = repository;

    public async Task<Sale> Handle(
        GetSaleByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.GetByIdAsync(
            request.Id
        );
    }
}