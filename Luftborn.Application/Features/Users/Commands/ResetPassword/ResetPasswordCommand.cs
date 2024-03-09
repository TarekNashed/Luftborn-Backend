﻿using MediatR;

namespace Luftborn.Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand:IRequest<UserManagerResponse>
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
