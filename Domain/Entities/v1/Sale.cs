using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.v1;

public class Sale(
    string customer,
    string branch
)
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string SaleNumber { get; private set; } = Guid.NewGuid().ToString()[..8];

    public DateTime Date { get; private set; } = DateTime.UtcNow;

    public string Customer { get; private set; } = customer;

    public string Branch { get; private set; } = branch;

    public bool IsCancelled { get; private set; }

    public List<SaleItem> Items { get; private set; } = new();

    public decimal TotalAmount => Items.Sum(x => x.Total);

    public void AddItem(
        string product,
        int quantity,
        decimal unitPrice
    )
    {
        EnsureCanModify();

        ValidateQuantity(quantity);

        var item = SaleItem.Create(
            product,
            quantity,
            unitPrice
        );

        Items.Add(item);
    }

    public void Cancel()
    {
        IsCancelled = true;
    }

    private void EnsureCanModify()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot modify a cancelled sale.");
    }

    private void ValidateQuantity(
        int quantity
    )
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Invalid item quantity.");

        if (quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");
    }
}