using Luftborn.Application.Model;

namespace Luftborn.Application.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
