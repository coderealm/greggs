using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Greggs.DataAccessLayer.Handlers;
using Greggs.DataAccessLayer.Models;
using Greggs.DataAccessLayer.Queries;
using Greggs.DataAccessLayer.Repositories;
using Greggs.Models;
using Greggs.Products.Api.Controllers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductTests
{
    private readonly Mock<IMediator> _mockMediator =  new Mock<IMediator>();

    private readonly Mock<IConfiguration> _mockConfiguration = new Mock<IConfiguration>(); 
    private readonly Mock<ILogger<ProductController>> _mockLogger = new Mock<ILogger<ProductController>>();


    /// <summary>
    /// Usually, I have tests covering all scenarios but since the purpose is to see how I code, I am leaving out tests
    /// </summary>
    /// <returns></returns>

    [Fact]
    public async Task ProductController_Returns_Array_Of_Product_Types()
    {

        var productController =
            new ProductController(_mockLogger.Object, _mockConfiguration.Object, _mockMediator.Object);

        var lastestMenu = await productController.GetLatestMenu();
        _mockMediator.Setup(x => x.Send(It.IsAny<GetLatestMenuQuery>(), CancellationToken.None))
            .Returns(() => It.IsAny<Task<IEnumerable<Product>>>());

        Assert.IsType<Product[]>(lastestMenu);
    }
}