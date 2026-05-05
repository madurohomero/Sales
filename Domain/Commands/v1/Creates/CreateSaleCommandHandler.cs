using AutoMapper;
using Domain.Commands.v1.Models.Creates;
using Domain.DomainServices;
using Domain.Entities.v1;
using Domain.Interfaces.v1.DomainServices;
using Domain.Interfaces.v1.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Creates;

public class CreateSaleCommandHandler(
    IMapper mapper,
    ISaleRepository saleRepository,
    ISaleDomainService saleDomainService,
    ILogger<CreateSaleCommandHandler> logger
) : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly IMapper _mapper = mapper;
    
    private readonly ISaleRepository _saleRepository = saleRepository;

    private readonly ISaleDomainService _saleDomainService = saleDomainService;

    private readonly ILogger<CreateSaleCommandHandler> _logger = logger;
    
    public async Task<Guid> Handle(
        CreateSaleCommand command,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation(
                "Creating sale for customer {Customer}",
                command.Customer
            );

            var saleEntity = _mapper.Map<Sale>(
                command
            );

            _saleDomainService.Validate(saleEntity);

            await _saleRepository.AddAsync(
                saleEntity
            );

            _logger.LogInformation(
                "Sale created successfully. SaleId: {SaleId}",
                saleEntity.Id
            );

            return saleEntity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error creating sale for customer {Customer}",
                command.Customer
            );
            
            throw;
        }
    }
}