using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ViewModels;

namespace TestApp.Validators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(p => p.ProductName).MaximumLength(15);
            RuleFor(p => p.Price).InclusiveBetween(10, 1000);
            RuleFor(p => p.Description).NotEmpty();
        }
    }
}
