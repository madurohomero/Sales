using MediatR;

namespace Domain.Commands.v1.Models.Creates;

public class CreateSaleCommand : IRequest<Guid>
{
    public string Customer { get; set; }
    public string Branch { get; set; }
    public List<SaleItemCommand> Items { get; set; }
}
