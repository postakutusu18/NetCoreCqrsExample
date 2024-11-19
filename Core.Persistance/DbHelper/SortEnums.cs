using System.ComponentModel.DataAnnotations;

public enum SortEnums
{
    [Display(Name = "OrderBy")]
    ASC = 1,

    [Display(Name = "OrderByDescending")]
    DESC = 2
}