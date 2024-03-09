using FluentValidation;

namespace Luftborn.Application.Features.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator:AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotNull().NotEmpty().WithMessage("UserId is required");

            RuleFor(p => p.Token)
                .NotNull().NotEmpty().WithMessage("Token is required");
        }
    }
}
