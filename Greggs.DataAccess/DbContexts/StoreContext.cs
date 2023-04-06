using Greggs.Models;
using Microsoft.EntityFrameworkCore;

namespace Greggs.DataAccess.DbContexts;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    { }

    public DbSet<Product> Products { get; set; }
}
