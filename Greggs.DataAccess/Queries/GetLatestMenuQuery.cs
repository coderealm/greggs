using Greggs.Models;
using MediatR;

namespace Greggs.DataAccess.Queries
{
    public record GetLatestMenuQuery(int PageStart = 0, int PageSize = 5) : IRequest<IEnumerable<Product>>;
}
