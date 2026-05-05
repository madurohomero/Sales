namespace Domain.Commands.v1.Models.Creates;

public class SaleItemCommand
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
