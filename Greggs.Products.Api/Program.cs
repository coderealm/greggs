using System;
using System.Linq;
using System.Threading.Tasks;
using Greggs.DataAccessLayer.DbContexts;
using Greggs.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Greggs.Models;
using Greggs.DataAccessLayer.Models;
using MediatR;

namespace Greggs.Products.Api;

public class Program
{
    private const string InMemoryDatabaseName = "Greggs";

    public static void Main(string[] args)
    {
        Task.Factory.StartNew(SeedProductsInMemoryDatabase);
        CreateHostBuilder(args).Build().Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        hostBuilder.ConfigureServices(
            services =>
            {
                services.AddDbContext<StoreContext>(options =>
                    options.UseInMemoryDatabase(databaseName: InMemoryDatabaseName));
                services.AddScoped(typeof(IRepository<>), typeof(StoreRepository<>));
                services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
                services.AddScoped<IPagination, Pagination>();
                services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            });

        return hostBuilder;
    }

    private static void SeedProductsInMemoryDatabase()
    {
        var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase(databaseName: InMemoryDatabaseName).Options;
        using var storeContext = new StoreContext(options);
        storeContext.Database.EnsureCreated();

        var sausageRoll = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Sausage Roll", StringComparison.CurrentCultureIgnoreCase));
        if (sausageRoll == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Sausage Roll", 
                Price = 1m, 
                Created = new DateTime(2008, 10, 12)
        });
        }

        var veganSausageRoll = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Vegan Sausage Roll", StringComparison.CurrentCultureIgnoreCase));
        if (veganSausageRoll == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Vegan Sausage Roll",
                Price = 1.1m,
                Created = new DateTime(2017, 8, 9)
            });
        }
        var steakBake = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Steak Bake", StringComparison.CurrentCultureIgnoreCase));
        if (steakBake == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Steak Bake",
                Price = 1.2m,
                Created = new DateTime(2018, 5, 20)
            });
        }
        var yumYum = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Yum Yum", StringComparison.CurrentCultureIgnoreCase));
        if (yumYum == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Yum Yum",
                Price = 0.7m,
                Created = new DateTime(2019, 11, 12)
            });
        }
        var pinkJammie = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Pink Jammie", StringComparison.CurrentCultureIgnoreCase));
        if (pinkJammie == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Pink Jammie",
                Price = 0.5m,
                Created = new DateTime(2020, 9, 14)
            });
        }
        var mexicanBaguette = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Mexican Baguette", StringComparison.CurrentCultureIgnoreCase));
        if (mexicanBaguette == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Mexican Baguette",
                Price = 2.1m,
                Created = new DateTime(2021, 7, 9)
            });
        }
        var  baconSandwich = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Bacon Sandwich", StringComparison.CurrentCultureIgnoreCase));
        if (baconSandwich == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Bacon Sandwich",
                Price = 1.95m,
                Created = new DateTime(2022, 10, 19)
            });
        }
        var  cocaCola = storeContext.Products.FirstOrDefault(product => product.Name.Equals("Coca Cola", StringComparison.CurrentCultureIgnoreCase));
        if (cocaCola == null)
        {
            storeContext.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString().Substring(0,8),
                Name = "Coca Cola",
                Price = 1.2m,
                Created = new DateTime(2023, 2, 12)
            });
        }
        storeContext.SaveChanges();
    }
}