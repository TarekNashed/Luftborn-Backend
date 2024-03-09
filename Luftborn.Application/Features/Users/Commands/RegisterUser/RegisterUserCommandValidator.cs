using FluentValidation;

namespace Luftborn.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .NotNull()
                .Length(3, 50).WithMessage("First name must be between 3 and 50 chars");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .NotNull()
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 chars");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email Address is required")
                .MaximumLength(50).WithMessage("Email must not exceed 50 chars")
                .NotNull().EmailAddress();

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull()
                .Length(6, 50).WithMessage("Password must be between 6 and 50 chars");

            RuleFor(p => p.ConfirmPassword)
                .Equal(p => p.Password).WithMessage("Password must be equal Confirm Password");

            RuleFor(p => p.RoleName).NotEmpty().WithMessage("Role name is required")
                .NotNull();
        }
    }
}
