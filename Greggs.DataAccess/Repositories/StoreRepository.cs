using System.Linq.Expressions;
using Greggs.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Greggs.DataAccess.Repositories;

public class StoreRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal StoreContext StoreContext;
    internal DbSet<TEntity> DbSet;
    public StoreRepository(StoreContext storeContext)
    {
        StoreContext = storeContext;
        DbSet = storeContext.Set<TEntity>();
    }
    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        { 
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new [] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }
}

