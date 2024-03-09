using FluentValidation;

namespace Luftborn.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(d => d.ID)
                .NotEmpty().WithMessage("Id of Dep cann't be empty").NotNull();
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name of Dep cann't be empty").NotNull().
                Length(3, 50);
            RuleFor(d => d.Code)
             .NotEmpty().WithMessage("Name of Code cann't be empty").NotNull().
             Length(2, 50);
            RuleFor(d => d.isActive)
             .NotEmpty().WithMessage("Please Check the Dep is Active or not").NotNull();
        }
    }
}
