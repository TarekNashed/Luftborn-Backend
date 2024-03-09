using Luftborn.Application.Infrastructure;
using Luftborn.Application.Model;
using Luftborn.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Luftborn.Application.Features.Users.Queries.UserLogin
{
    public record UserLoginQuery(string Email, string Password):IRequest<UserManagerResponse>;
    public class UserLoginHandler:IRequestHandler<UserLoginQuery, UserManagerResponse>
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly ILogger<UserLoginHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserLoginHandler(UserManager<ApplicationUser> userManger, ILogger<UserLoginHandler> logger, IEmailService emailService, IConfiguration configuration)
        {
            _userManger = userManger ?? throw new ArgumentNullException(nameof(userManger));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<UserManagerResponse> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = await _userManger.CheckPasswordAsync(user, request.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var roles = await _userManger.GetRolesAsync(user);
            JwtSecurityToken token = GetToken(user, roles);

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            await SendEmail(request.Email);
            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        private JwtSecurityToken GetToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new[]
                        {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roles[0]),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:SigningKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issure"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["AuthSettings:ExpiryInHours"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return token;
        }

        private async Task SendEmail(string emailAddress)
        {
            var email = new Email
            {
                To = emailAddress,
                Subject = "New login",
                Body = $"<h1>Hey!, new login to your account noticed</h1><p>New login to your account at {DateTime.Now} </p>"
            };
            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"New login email {emailAddress} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
