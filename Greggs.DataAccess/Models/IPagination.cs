namespace Greggs.DataAccess.Models;

public class Pagination : IPagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public Pagination Page(int pageNumber, int pageSize, int totalProducts)
    {
        return new Pagination
        {
            PageNumber = pageNumber < 0 ? 0 : pageNumber,
            PageSize = pageSize > totalProducts ? totalProducts : pageSize
        };
    }
}

public interface IPagination
{
    Pagination Page(int pageNumber, int pageSize, int totalProducts);
}

