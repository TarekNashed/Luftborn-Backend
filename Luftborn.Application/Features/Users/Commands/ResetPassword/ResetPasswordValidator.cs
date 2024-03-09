using FluentValidation;

namespace Luftborn.Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(p => p.Token)
                .NotNull().NotEmpty().WithMessage("Token is required");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("UserName is required")
                .NotNull()
                .Length(3, 50).WithMessage("UserName must be between 3 and 50 chars");

            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .NotNull()
                .Length(6, 50).WithMessage("Password must be between 6 and 50 chars");

            RuleFor(p => p.ConfirmPassword)
                .Equal(p => p.NewPassword).WithMessage("New Password must be equal Confirm Password");
        }
    }
}
