namespace JWTAuthAPI.Interfaces
{
    public interface IRepositoryActivator : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
    }
}
