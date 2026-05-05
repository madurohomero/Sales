using Domain.Entities.v1;

namespace Domain.Interfaces.v1.Repositories;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);
    Task<Sale> GetByIdAsync(Guid id);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(Guid id);
}