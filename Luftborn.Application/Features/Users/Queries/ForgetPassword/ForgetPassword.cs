using Luftborn.Application.Infrastructure;
using Luftborn.Application.Model;
using Luftborn.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Luftborn.Application.Features.Users.Queries.ForgetPassword
{
    public record ForgetPasswordQuery(string Email) : IRequest<UserManagerResponse>;
    public class ForgetPasswordHandler:IRequestHandler<ForgetPasswordQuery,UserManagerResponse>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ForgetPasswordHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;

        public ForgetPasswordHandler(IEmailService emailService, ILogger<ForgetPasswordHandler> logger, UserManager<ApplicationUser> userManger, IConfiguration configuration)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManger = userManger ?? throw new ArgumentNullException(nameof(userManger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<UserManagerResponse> Handle(ForgetPasswordQuery request, CancellationToken cancellationToken)
        {
            var identityUser = await _userManger.FindByEmailAsync(request.Email);
            if (identityUser == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            var passwordResetToken = await _userManger.GeneratePasswordResetTokenAsync(identityUser);
            var encodedToken = Encoding.UTF8.GetBytes(passwordResetToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            await SendEmail(identityUser, validEmailToken);
            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }

        private async Task SendEmail(ApplicationUser identityUser, string validEmailToken)
        {
            string url = $"{_configuration["ApiSettings:AppUrl"]}/ResetPassword?email={identityUser.Email}&token={validEmailToken}";

            var email = new Email
            {
                To = identityUser.Email,
                Subject = "Reset Password",
                Body = $"<h1>Follow the instructions to reset your password</h1><p>To reset your password <a href='{url}'>Click here</a></p>"
            };
            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Resetting password email {identityUser.Email} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
