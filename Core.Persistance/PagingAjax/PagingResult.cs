namespace Core.Persistance.PagingAjax;

public class PagingResult<T> : IPagingResult<T>
{
    public PagingResult(List<T> data, int totalItemCount):base()
    {
        Data = data;
        TotalItemCount = totalItemCount;
    }
    public List<T> Data { get; }
    public int TotalItemCount { get; }
}