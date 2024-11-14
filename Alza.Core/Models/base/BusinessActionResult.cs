using Alza.Core.BusinessResult;
using FluentValidation.Results;

public class BusinessActionResult<TResult>
{
    public TResult Data { get; set; }
    public string Code { get; set; }
    public object Parameters { get; set; }
    public bool IsSuccess => Code == "Success" || Code == "Created";

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

    public BusinessActionResult(TResult result, string code, int createdId)
    {
        Data = result;
        Code = code;
        Parameters = createdId;
    }

    public static implicit operator BusinessActionResult<TResult>(TResult value) => 
        new BusinessActionResult<TResult>(value);

    public static implicit operator BusinessActionResult<TResult>(EntityNotFoundMessage entityNotFoundMessage) =>
        new BusinessActionResult<TResult>("EntityNotFound", entityNotFoundMessage.ToString());
  

    public static implicit operator BusinessActionResult<TResult>(ValidationResult validationResult) =>
        new BusinessActionResult<TResult>("ValidationFailed", validationResult.Errors);
}
