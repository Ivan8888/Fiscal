using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.ViewModels;

namespace TestApp.Validators
{
    public class SupplierViewModelValidator : AbstractValidator<SupplierViewModel>
    {
        public SupplierViewModelValidator(SiteContext context)
        {
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(15)
                .MustAsync(async (value, a) => {
                     return !(await context.Suppliers.AnyAsync(s => s.Name == value));
                 })
                .WithMessage("Name must be unique value in database!");

            When(s => !string.IsNullOrEmpty(s.OfficeAddress) || 
                      !string.IsNullOrEmpty(s.Email), 
                      () => {
                          RuleFor(s => s.OfficeAddress).NotEmpty().WithMessage("Office address can't be empty becase email not empty");
                          RuleFor(s => s.Email).NotEmpty().WithMessage("Email can't be empty becase office address not empty");
                      });
        }
    }
}
