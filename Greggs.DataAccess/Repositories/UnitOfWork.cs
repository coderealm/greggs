namespace Greggs.DataAccess.Repositories;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
{
    public UnitOfWork(IRepository<TEntity> storeRepository)
    {
        StoreRepository = storeRepository;
    }

    public IRepository<TEntity> StoreRepository { get; }
}