using BuildingBlocks.Shared.Entities;

namespace BuildingBlocks.Persistence.Interfaces
{
    public interface IBaseSQLServerRepository<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync(int pageNumber = 1, int pageSize = 20);
        Task<T?> GetByIdAsync(int id);
    }
}
