using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftborn.Application.Features.Users.Queries.UserLogin
{
    public class UserLoginValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("UserName is required")
                .NotNull()
                .Length(3, 50).WithMessage("UserName must be between 3 and 50 chars");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull()
                .Length(6, 50).WithMessage("Password must be between 6 and 50 chars");
        }
    }
}
