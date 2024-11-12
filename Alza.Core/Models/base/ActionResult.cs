using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Core.Models;

public class ActionResult<TResult>
{
    public TResult Data { get; set; }

    public string Code { get; set; }

    public object Parameters { get; set; }

    public bool IsSuccess => Code == "Success";

    public ActionResult(string code, object parameters = null)
    {
        Code = code;
        Parameters = parameters;
    }

    public ActionResult(TResult result)
    {
        Data = result;
        Code = "Success";
    }
}
