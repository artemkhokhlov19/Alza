using Alza.Contracts.DataObjects.Products;
using FluentValidation;

namespace Alza.Contracts.DataObjects.Validators;

public class ProductCreateModelValidator : AbstractValidator<ProductCreateModel>
{
    public ProductCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");
        
        RuleFor(x => x.ImgUri)
            .NotEmpty()
            .WithMessage("Product image URI is required.");        

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Product price is required.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
            });

    }
    

}
