using Domain.Commands.v1.Models.Cancels;
using Domain.Interfaces.v1.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Cancels;

public class CancelSaleCommandHandler(
    ISaleRepository repository,
    ILogger<CancelSaleCommandHandler> logger
) : IRequestHandler<CancelSaleCommand>
{
    private readonly ISaleRepository _repository = repository;
    
    private readonly ILogger<CancelSaleCommandHandler> _logger = logger;
    
    public async Task Handle(
        CancelSaleCommand command,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation(
            "Starting cancel sale. SaleId: {SaleId}",
            command.Id
        );
        
        var sale = await _repository.GetByIdAsync(
            command.Id
        );

        if (sale is null)
        {
            _logger.LogWarning(
                "Sale not found. SaleId: {SaleId}",
                command.Id
            );
            
            throw new Exception("Venda não encontrada");
        }
        
        sale.Cancel();

        await _repository.UpdateAsync(
            sale
        );
        
        _logger.LogInformation(
            "Sale cancelled successfully. SaleId: {SaleId}",
            command.Id
        );
    }
}