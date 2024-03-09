using Luftborn.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Luftborn.Application.Features.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler:IRequestHandler<ConfirmEmailCommand,UserManagerResponse>
    {
        private UserManager<ApplicationUser> _userManger;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger ?? throw new ArgumentNullException(nameof(userManger));
        }

        public async Task<UserManagerResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByIdAsync(request.UserId);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
