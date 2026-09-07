namespace HMS.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailVerificationAsync(string email, string token, CancellationToken ct = default);
    Task SendPasswordResetAsync(string email, string token, CancellationToken ct = default);
}
