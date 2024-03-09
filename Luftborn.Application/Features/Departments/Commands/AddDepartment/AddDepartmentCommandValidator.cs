using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftborn.Application.Features.Departments.Commands.AddDepartment
{
    public class AddDepartmentCommandValidator:AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotNull().NotEmpty().MinimumLength(2).WithMessage("Code is required with Minmum length 3 digits");
            RuleFor(d => d.Name)
                .NotNull().NotEmpty().MinimumLength(3).WithMessage("Name is required with Minmum length 3 digits");
        }
    }
}
