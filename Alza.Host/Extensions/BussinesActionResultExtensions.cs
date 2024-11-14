using Alza.Core.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Alza.Host.Extensions;

public static class BussinesActionResultExtensions
{
    /// <summary>
    /// Converts BussinesActionResult to ActionResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <param name="customCases"></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this BusinessActionResult<T> result, IDictionary<string, IActionResult> customCases = null)
    {
        if (result == null)
        {
            return null;
        }

        if (result.IsSuccess)
        {
            if (result.Data != null)
            {
                return new OkObjectResult(result.Data);
            }
            return new OkResult();
        }

        Dictionary<string, IActionResult> dictionary = new Dictionary<string, IActionResult>
        {
            {
                "EntityNotFound",
                new NotFoundObjectResult(result.Parameters)
                {
                    ContentTypes = HttpProblemDetailsStandard()
                }
            }
        };

        if (result.Code == "ValidationFailed")
        {
            var errors = new Dictionary<string, List<string>>();

            var validationErrors = result.Parameters as IEnumerable<ValidationFailure>;
            if (validationErrors != null)
            {
                foreach (var error in validationErrors)
                {
                    if (!errors.ContainsKey(error.PropertyName))
                    {
                        errors[error.PropertyName] = new List<string>();
                    }

                    errors[error.PropertyName].Add(error.ErrorMessage);
                }
            }

            return new UnprocessableEntityObjectResult(
                new
                {
                    type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.21",
                    title = "One or more validation errors occurred.",
                    status = 422,
                    errors
                });
        }

        var combinedCases = customCases != null
            ? dictionary.Concat(customCases).ToDictionary(kv => kv.Key, kv => kv.Value)
            : dictionary;

        if (!combinedCases.TryGetValue(result.Code, out var actionResult))
        {
            return new BadRequestObjectResult(result.Code.ToString());
        }

        return actionResult;
    }

    private static MediaTypeCollection HttpProblemDetailsStandard()
    {
        return new MediaTypeCollection { "application/problem+json" };
    }
}
