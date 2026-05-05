using AutoMapper;
using Domain.Commands.v1.Models.Creates;
using Domain.Entities.v1;

namespace Domain.MapperProfiles.v1;

public class SalesProfile : Profile
{
    public SalesProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ConstructUsing(src => new Sale(
                src.Customer,
                src.Branch
            ));

        CreateMap<SaleItemCommand, SaleItem>()
            .ConstructUsing(src => SaleItem.Create(
                src.Product,
                src.Quantity,
                src.UnitPrice
            ));
    }
}