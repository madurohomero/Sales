using Domain.Entities.v1;
using Domain.Interfaces.v1.DomainServices;

namespace Domain.DomainServices;

public class SaleDomainService : ISaleDomainService
{
    public void Validate(Sale sale)
    {
        if (sale == null)
            throw new InvalidOperationException("Sale cannot be null.");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Cancelled sale cannot be processed.");

        if (sale.Items == null || !sale.Items.Any())
            throw new InvalidOperationException("Sale must contain at least one item.");

        foreach (var item in sale.Items)
        {
            ValidateItem(item);
        }
    }

    private static void ValidateItem(SaleItem item)
    {
        if (item.Quantity <= 0)
            throw new InvalidOperationException("Item quantity must be greater than zero.");

        if (item.Quantity > 20)
            throw new InvalidOperationException(
                $"Item {item.Product} exceeds maximum allowed quantity (20)."
            );

        if (string.IsNullOrWhiteSpace(item.Product))
            throw new InvalidOperationException("Item product is required.");

        if (item.UnitPrice <= 0)
            throw new InvalidOperationException("Item unit price must be greater than zero.");
    }
}