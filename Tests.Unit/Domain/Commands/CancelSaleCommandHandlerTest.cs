using Domain.Commands.v1.Cancels;
using Domain.Commands.v1.Models.Cancels;
using Domain.Entities.v1;
using Domain.Interfaces.v1.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Unit.Domain.Commands;

public class CancelSaleCommandHandlerTest
{
    private readonly Mock<ISaleRepository> _repository;
    private readonly Mock<ILogger<CancelSaleCommandHandler>> _logger;
    private readonly CancelSaleCommandHandler _handler;

    public CancelSaleCommandHandlerTest()
    {
        _repository = new Mock<ISaleRepository>();
        _logger = new Mock<ILogger<CancelSaleCommandHandler>>();

        _handler = new CancelSaleCommandHandler(
            _repository.Object,
            _logger.Object
        );
    }

    [Test]
    public async Task Should_Cancel_Sale_Successfully()
    {
        var saleId = Guid.NewGuid();

        var sale = new Sale("João", "Guariba");

        _repository
            .Setup(x => x.GetByIdAsync(saleId))
            .ReturnsAsync(sale);

        _repository
            .Setup(x => x.UpdateAsync(It.IsAny<Sale>()))
            .Returns(Task.CompletedTask);

        var command = new CancelSaleCommand
        {
            Id = saleId
        };

        await _handler.Handle(
            command,
            CancellationToken.None
        );

        sale.IsCancelled.Should().BeTrue();

        _repository.Verify(x => x.GetByIdAsync(saleId), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Sale>()), Times.Once);
    }

    [Test]
    public async Task Should_Throw_Exception_When_Sale_Not_Found()
    {
        var saleId = Guid.NewGuid();

        _repository
            .Setup(x => x.GetByIdAsync(saleId))
            .ReturnsAsync((Sale)null);

        var command = new CancelSaleCommand
        {
            Id = saleId
        };

        Func<Task> act = async () =>
            await _handler.Handle(command, CancellationToken.None);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Venda não encontrada");

        _repository.Verify(x => x.UpdateAsync(It.IsAny<Sale>()), Times.Never);
    }
}
