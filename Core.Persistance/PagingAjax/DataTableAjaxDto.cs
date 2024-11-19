namespace Core.Persistance.PagingAjax;

public class DataTableAjaxDto : IDataTableAjaxDto
{
    public DataTableAjaxDto()
    {
        //Default Settings
        First = 0;
        Rows = 5;
        SortField = "Id";
        //IsActive = true;
        IsDelete = false;
        //IsState = false;
        Level = "all";
    }

    public int First { get; set; }
    public int Rows { get; set; }
    public string? SortField { get; set; }
    public int SortOrder { get; set; }
    public string? SearchText { get; set; }
    public List<string>? PropertyField { get; set; }
    public string? Level { get; set; }
    public string? DateStart { get; set; }
    public string? DateEnd { get; set; }
    //public bool IsState { get; set; }
    public bool? IsActive { get; set; }
    public bool IsDelete { get; set; }

}