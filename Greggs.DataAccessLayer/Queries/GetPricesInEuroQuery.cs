using Greggs.Models;
using MediatR;

namespace Greggs.DataAccessLayer.Queries;

public record GetPricesInEuroQuery(decimal ExchangeRate, int PageStart = 0, int PageSize = 5) : IRequest<IEnumerable<Product>>;