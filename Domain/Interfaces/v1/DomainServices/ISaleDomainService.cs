using Domain.Entities.v1;

namespace Domain.Interfaces.v1.DomainServices;

public interface ISaleDomainService
{
    void Validate(Sale sale);
}