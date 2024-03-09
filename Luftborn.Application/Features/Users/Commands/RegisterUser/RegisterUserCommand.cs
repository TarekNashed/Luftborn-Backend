﻿using MediatR;

namespace Luftborn.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand:IRequest<UserManagerResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string RoleName { get; set; }
    }
}
