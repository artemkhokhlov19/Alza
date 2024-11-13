namespace Alza.Core.Models;

public class PagedList<T>
{
    public int CurrentPage { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public List<T> Data { get; set; } = new List<T>();

    internal PagedList(IEnumerable<T> data, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        Data.AddRange(data);
    }
}
