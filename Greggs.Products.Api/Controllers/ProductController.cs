using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greggs.DataAccessLayer.Queries;
using Greggs.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

/// <summary>
/// This is not production ready but shows how I code
/// </summary>

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;


    public ProductController(ILogger<ProductController> logger,
        IConfiguration configuration, IMediator mediator)
    {
        _logger = logger;
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpGet, Route("menu/get-latest")]
    public async Task<IEnumerable<Product>> GetLatestMenu(int pageStart = 0, int pageSize = 5)
    {
        try
        {
            return await _mediator.Send(new GetLatestMenuQuery(pageStart, pageSize));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    [HttpGet, Route("get-prices-in-euro")]
    public async Task<IEnumerable<Product>> GetEuroPrice(int pageStart = 0, int pageSize = 5)
    {
        try
        {
            var exchangeRate = Convert.ToDecimal(_configuration.GetSection("ExchangeRate").Value);
            return await _mediator.Send(new GetPricesInEuroQuery(exchangeRate, pageStart, pageSize));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}