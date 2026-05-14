using FluentValidation;

namespace RestAprilEducation.Application.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Must(productName => productName.StartsWith("A"))
                .WithMessage("Name must start with letter A");    

            RuleFor(x => x.Price)
                .InclusiveBetween(1, 1000).WithMessage("Price must be between 1 and 1000");

        }
    }
}
