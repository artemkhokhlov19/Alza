using Alza.Core.BusinessResult;

namespace Alza.Core.Models;

public class BusinessActionResult<TResult>
{
    public TResult Data { get; set; }

    public string Code { get; set; }

    public object Parameters { get; set; }

    public bool IsSuccess => Code == "Success";

    public BusinessActionResult(string code, object parameters = null)
    {
        Code = code;
        Parameters = parameters;
    }

    public BusinessActionResult(TResult result)
    {
        Data = result;
        Code = "Success";
    }

    public static implicit operator BusinessActionResult<TResult>(TResult value)
    {
        return new BusinessActionResult<TResult>(value);
    }

    public static implicit operator BusinessActionResult<TResult>(EntityNotFoundMessage entityNotFoundMessage)
    {
        return new BusinessActionResult<TResult>("EntityNotFound", entityNotFoundMessage.ToString());
    }
}
