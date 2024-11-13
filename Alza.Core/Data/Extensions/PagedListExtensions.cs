using Alza.Core.Models;

namespace Alza.Core.Data.Extensions;

public static class PagedListExtensions
{
    private const int DefaultLimit = 10;
    private const int MaxLimit = 100;

    public static int GetLimit(this PagedRequest request)
    {
        var limit = request?.Limit ?? DefaultLimit;
        if (limit > MaxLimit) limit = MaxLimit;
        return limit;
    }

    public static int GetOffset(this PagedRequest request) => request.Offset ?? 0;

    public static int GetPage(this PagedRequest request) => (request.GetOffset() / request.GetLimit()) + 1;

    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize, int total)
    {
        return new PagedList<T>(source, total, pageNumber, pageSize);
    }
}
