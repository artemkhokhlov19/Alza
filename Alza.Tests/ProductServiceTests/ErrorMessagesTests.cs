using Alza.Appllication.Utils;
using FluentValidation.Results;
using NUnit.Framework;

namespace Alza.Tests.ProductServiceTests;

[TestFixture]
public class ErrorMessagesTests
{
    [Test]
    public void CombineErrorMessages_ValidErrors_ReturnsCombinedMessage()
    {
        var errors = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required."),
            new ValidationFailure("Price", "Price must be a positive number."),
            new ValidationFailure("ImgUri", "ImgUri must be not empty.")
        };

        var expectedMessage =
            "Name: Name is required.\r\n" +
            "Price: Price must be a positive number.\r\n" +
            "ImgUri: ImgUri must be not empty.\r\n";

        var result = LoggerUtils.CombineErrorMessages(errors);

        Assert.That(result, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void CombineErrorMessages_EmptyErrors_ReturnsEmptyString()
    {
        var errors = new List<ValidationFailure>();

        var result = LoggerUtils.CombineErrorMessages(errors);

        Assert.That(result, Is.EqualTo(string.Empty));
    }
}
