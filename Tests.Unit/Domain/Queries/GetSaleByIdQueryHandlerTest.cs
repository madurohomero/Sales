using Domain.Entities.v1;
using Domain.Interfaces.v1.Repositories;
using Domain.Queries.v1.Models;
using Domain.Queries.v1;
using Moq;
using FluentAssertions;

namespace Tests.Unit.Domain.Queries;

public class GetSaleByIdQueryHandlerTest
{
    private readonly Mock<ISaleRepository> _repository;
    private readonly GetSaleByIdQueryHandler _handler;

    public GetSaleByIdQueryHandlerTest()
    {
        _repository = new Mock<ISaleRepository>();

        _handler = new GetSaleByIdQueryHandler(
            _repository.Object
        );
    }

    [Test]
    public async Task Should_Return_Sale_When_Id_Exists()
    {
        var saleId = Guid.NewGuid();

        var sale = new Sale(
            "João da Silva",
            "Guariba"
        );

        _repository
            .Setup(x => x.GetByIdAsync(saleId))
            .ReturnsAsync(sale);

        var query = new GetSaleByIdQuery
        {
            Id = saleId
        };

        var result = await _handler.Handle(
            query,
            CancellationToken.None
        );

        result.Should().NotBeNull();
        result.Should().Be(sale);

        _repository.Verify(
            x => x.GetByIdAsync(saleId),
            Times.Once
        );
    }
}