namespace DG.DAL.Entities;
public class PagedList<T>
{
    public IEnumerable<T>? Collection { get; set; }
    public int TotalPages { get; set; }
    public PagedList(IEnumerable<T>? collection, int totalPages)
    {
        Collection = collection;
        TotalPages = totalPages;
    }
}