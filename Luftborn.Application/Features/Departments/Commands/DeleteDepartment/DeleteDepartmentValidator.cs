using FluentValidation;
using Luftborn.Application.Model;

namespace Luftborn.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentValidator:AbstractValidator<DeleteDepartmentHandlerQuery>
    {
        public DeleteDepartmentValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id of Department cann't be empty").NotNull();
        }
    }
}
