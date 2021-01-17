using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ViewModels;

namespace TestApp.Validators
{
    public class SupplierVewModelValidator : AbstractValidator<SupplierViewModel>
    {
        public SupplierVewModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(15);
        }
    }
}
