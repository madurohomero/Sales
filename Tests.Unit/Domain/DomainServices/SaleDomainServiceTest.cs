using Domain.DomainServices;
using Domain.Entities.v1;
using FluentAssertions;

namespace Tests.Unit.Domain.DomainServices;

public class SaleDomainServiceTest
{
    private readonly SaleDomainService _service = new();

    private static Sale CreateValidSale()
    {
        var sale = new Sale(
            "João",
            "Guariba"
        );

        sale.AddItem(
            "Mouse",
            2,
            100m
        );

        return sale;
    }

    [Test]
    public void Should_Validate_Sale_Successfully()
    {
        var sale = CreateValidSale();

        Action act = () => _service.Validate(sale);

        act.Should().NotThrow();
    }

    [Test]
    public void Should_Throw_When_Sale_Is_Null()
    {
        Sale sale = null;

        Action act = () => _service.Validate(sale);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Sale cannot be null.");
    }

    [Test]
    public void Should_Throw_When_Sale_Is_Cancelled()
    {
        var sale = CreateValidSale();

        sale.Cancel();

        Action act = () => _service.Validate(sale);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Cancelled sale cannot be processed.");
    }
}
