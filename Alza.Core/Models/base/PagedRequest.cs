using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Core.Models;

public class PagedRequest
{
    private int? limit;

    [Range(0, int.MaxValue)]
    public int? Offset {  get; set; }

    public int Limit
    {
        get
        {
            if (!limit.HasValue) 
            {
                return int.MaxValue;            
            }

            return limit.Value;
        }

        set 
        {
            limit = ((value == int.MaxValue)) ? null : new int?(value);
        }
    }
}
