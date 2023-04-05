using Greggs.DataAccessLayer.Models;
using Greggs.DataAccessLayer.Queries;
using Greggs.DataAccessLayer.Repositories;
using Greggs.Models;
using MediatR;

namespace Greggs.DataAccessLayer.Handlers;

public class GetPricesInEuroHandler : IRequestHandler<GetPricesInEuroQuery, IEnumerable<Product>>
{
    private readonly IUnitOfWork<Product> _unitOfWork;
    private readonly IPagination _pagination;
    public GetPricesInEuroHandler(IUnitOfWork<Product> unitOfWork, IPagination pagination)
    {
        _unitOfWork = unitOfWork;
        _pagination = pagination;
    }

    public async Task<IEnumerable<Product>> Handle(GetPricesInEuroQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.StoreRepository
                                                .GetAsync(orderBy: product => product.OrderByDescending(x => x.Price));

        if (products != null && products.Any())
        {
            var productPricesInEuro = products.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = Math.Round(product.Price * request.ExchangeRate, 2),
                Created = product.Created
            });

            var pagination = _pagination.Page(request.PageStart, request.PageSize, products.Count());

            return productPricesInEuro.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
        return Array.Empty<Product>();
    }
}

