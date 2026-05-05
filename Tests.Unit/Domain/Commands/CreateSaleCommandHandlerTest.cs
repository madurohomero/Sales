using AutoMapper;
using Domain.Commands.v1.Creates;
using Domain.Commands.v1.Models.Creates;
using Domain.Entities.v1;
using Domain.Interfaces.v1.DomainServices;
using Domain.Interfaces.v1.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Unit.Domain.Commands;

public class CreateSaleCommandHandlerTest
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ISaleRepository> _saleRepository;
    private readonly Mock<ISaleDomainService> _saleDomainService;
    private readonly Mock<ILogger<CreateSaleCommandHandler>> _logger;

    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTest()
    {
        _mapper = new Mock<IMapper>();
        _saleRepository = new Mock<ISaleRepository>();
        _saleDomainService = new Mock<ISaleDomainService>();
        _logger = new Mock<ILogger<CreateSaleCommandHandler>>();

        _handler = new CreateSaleCommandHandler(
            _mapper.Object,
            _saleRepository.Object,
            _saleDomainService.Object,
            _logger.Object
        );
    }

    [Test]
    public async Task Should_Create_Sale_And_Save_Successfully()
    {
        var command = new CreateSaleCommand
        {
            Customer = "João da Silva",
            Branch = "Guariba",
            Items =
            [
                new SaleItemCommand
                {
                    Product = "Mouse Gamer",
                    Quantity = 10,
                    UnitPrice = 100m
                }
            ]
        };

        var sale = new Sale("João da Silva", "Guariba");

        _mapper
            .Setup(x => x.Map<Sale>(command))
            .Returns(sale);

        _saleRepository
            .Setup(x => x.AddAsync(It.IsAny<Sale>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(
            command,
            CancellationToken.None
        );

        result.Should().NotBe(Guid.Empty);

        _saleDomainService.Verify(
            x => x.Validate(It.IsAny<Sale>()),
            Times.Once
        );

        _saleRepository.Verify(
            x => x.AddAsync(It.IsAny<Sale>()),
            Times.Once
        );
    }
}