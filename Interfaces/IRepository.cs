namespace JWTAuthAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id, 
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> ListAllAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, 
            CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, 
            CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, 
            CancellationToken cancellationToken = default);
    }
}
