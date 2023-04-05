using Greggs.DataAccessLayer.Models;
using Greggs.DataAccessLayer.Queries;
using Greggs.DataAccessLayer.Repositories;
using Greggs.Models;
using MediatR;

namespace Greggs.DataAccessLayer.Handlers
{
    public class GetLatestMenuHandler : IRequestHandler<GetLatestMenuQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        private readonly IPagination _pagination;
        public GetLatestMenuHandler(IUnitOfWork<Product> unitOfWork, IPagination pagination)
        {
            _unitOfWork = unitOfWork;
            _pagination = pagination;
        }
        public async Task<IEnumerable<Product>> Handle(GetLatestMenuQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.StoreRepository
                                                    .GetAsync(orderBy: product => product.OrderByDescending(x => x.Created));
            if (products != null && products.Any())
            {
                var pagination = _pagination.Page(request.PageStart, request.PageSize, products.Count());

                return products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize);
            }
            return Array.Empty<Product>();
        }
    }
}
