using FluentValidation.Results;
using System.Text;

namespace Alza.Appllication.Utils;

public static class LoggerUtils
{
    public static string CombineErrorMessages(IEnumerable<ValidationFailure> errors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var error in errors)
        {
            sb.AppendLine($"{error.PropertyName}: {error.ErrorMessage}");
        }
        return sb.ToString();
    }
}
