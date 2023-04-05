namespace Greggs.DataAccessLayer.Repositories;

public interface IUnitOfWork<TEntity> where TEntity: class
{
    IRepository<TEntity> StoreRepository { get; }
}
