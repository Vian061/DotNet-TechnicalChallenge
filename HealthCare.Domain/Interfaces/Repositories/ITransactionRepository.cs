namespace HealthCare.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository<T>
    {
        Task<T> CreateAsync(T datum);
        Task<T> UpdateAsync(T datum);

    }
}
