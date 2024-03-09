using Luftborn.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Luftborn.Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordHandler:IRequestHandler<ResetPasswordCommand,UserManagerResponse>
    {
        private UserManager<ApplicationUser> _userManger;

        public ResetPasswordHandler(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger ?? throw new ArgumentNullException(nameof(userManger));
        }

        public async Task<UserManagerResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByEmailAsync(request.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            if (request.NewPassword != request.ConfirmPassword)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Password doesn't match its confirmation",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ResetPasswordAsync(user, normalToken, request.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }
    }
}
