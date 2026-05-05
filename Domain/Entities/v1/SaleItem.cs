namespace Domain.Entities.v1;


public class SaleItem
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }

    private SaleItem() { }

    private SaleItem(
        string product,
        int quantity,
        decimal unitPrice,
        decimal discount,
        decimal total
    )
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        Total = total;
    }

    public static SaleItem Create(
        string product,
        int quantity,
        decimal unitPrice
    )
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");

        if (quantity > 20)
            throw new InvalidOperationException("Maximum quantity allowed is 20.");

        var subtotal = quantity * unitPrice;
        var discount = CalculateDiscount(quantity, subtotal);
        var total = subtotal - discount;

        return new SaleItem(
            product,
            quantity,
            unitPrice,
            discount,
            total
        );
    }

    private static decimal CalculateDiscount(
        int quantity,
        decimal subtotal
    )
    {
        if (quantity < 4)
            return 0;

        if (quantity >= 10)
            return subtotal * 0.20m;

        return subtotal * 0.10m;
    }
}