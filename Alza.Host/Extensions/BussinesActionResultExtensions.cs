using Alza.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Alza.Host.Extensions;

public static class BussinesActionResultExtensions
{
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
        if (!((customCases != null) ? dictionary.Concat(customCases).ToDictionary((KeyValuePair<string, IActionResult> kv) => kv.Key, (KeyValuePair<string, IActionResult> kv) => kv.Value) : dictionary).TryGetValue(result.Code, out var value))
        {
            return new BadRequestObjectResult(result.Code.ToString());
        }

        return value;
    }

    private static MediaTypeCollection HttpProblemDetailsStandard()
    {
        return new MediaTypeCollection { "application/problem+json" };
    }
}
