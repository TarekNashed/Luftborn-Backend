using AutoMapper;
using Luftborn.Application.Infrastructure;
using Luftborn.Application.Model;
using Luftborn.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Luftborn.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;

        public RegisterUserCommandHandler(IMapper mapper, IEmailService emailService, ILogger<RegisterUserCommandHandler> logger, UserManager<ApplicationUser> userManger, IConfiguration configuration)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManger = userManger ?? throw new ArgumentNullException(nameof(userManger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<UserManagerResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.Password != request.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };

            var identityUser = _mapper.Map<ApplicationUser>(request);
            identityUser.UserName = identityUser.Email;
            identityUser.SecurityStamp = new Guid().ToString();
            var result = await _userManger.CreateAsync(identityUser, request.Password);

            if (result.Succeeded)
            {
                await _userManger.AddToRoleAsync(identityUser, request.RoleName);

                var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                _logger.LogInformation($"Registering user {identityUser.Id} ({identityUser.UserName}) is created successfully");

                await SendEmail(identityUser, validEmailToken);

                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            _logger.LogInformation($"Registering user {identityUser.Id} ({identityUser.UserName}) failed");

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        private async Task SendEmail(ApplicationUser identityUser, string validEmailToken)
        {
            string url = $"{_configuration["ApiSettings:AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

            var email = new Email
            {
                To = identityUser.Email,
                Subject = "Confirm your email",
                Body = $"<h1>Welcome to Auth Demo</h1><p>Please confirm your email by <a href='{url}'>Clicking here</a></p>"
            };
            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Confirming email {identityUser.Email} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
