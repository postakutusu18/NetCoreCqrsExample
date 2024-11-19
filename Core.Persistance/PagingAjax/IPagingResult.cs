namespace Core.Persistance.PagingAjax;

public interface IPagingResult<T> 
{
    List<T> Data { get; }
    int TotalItemCount { get; }
}
