using System.ComponentModel.DataAnnotations;

namespace Alza.Core.Models;

public class PagedRequest
{
    private int? limit;

    [Range(0, int.MaxValue)]
    public int? Offset {  get; set; }

    public int? Limit
    {
        get
        {
            if (!limit.HasValue) 
            {
                return null;            
            }

            return limit.Value;
        }

        set 
        {
            limit = ((value == int.MaxValue)) ? null : new int?(value ?? 0);
        }
    }
}
