using System.Net;
using System.Net.Mail;
using HMS.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HMS.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _fromAddress;
    private readonly string _fromName;
    private readonly string _password;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _smtpHost = configuration["Email:SmtpHost"] ?? "smtp.gmail.com";
        _smtpPort = int.TryParse(configuration["Email:SmtpPort"], out var port) ? port : 587;
        _fromAddress = configuration["Email:FromAddress"]
            ?? throw new InvalidOperationException("Email:FromAddress not configured.");
        _fromName = configuration["Email:FromName"] ?? "HMS Hospital";
        _password = configuration["Email:Password"] ?? string.Empty;
        _logger = logger;
    }

    public async Task SendEmailVerificationAsync(string email, string token, CancellationToken ct = default)
    {
        var subject = "HMS — Verify Your Email Address";
        var body = $"""
            <h2>Welcome to HMS!</h2>
            <p>Please verify your email address by clicking the link below:</p>
            <p><a href="https://your-domain.com/verify?token={token}">Verify Email</a></p>
            <p>If you did not create an account, you can safely ignore this email.</p>
            """;

        await SendAsync(email, subject, body, ct);
    }

    public async Task SendPasswordResetAsync(string email, string token, CancellationToken ct = default)
    {
        var subject = "HMS — Reset Your Password";
        var body = $"""
            <h2>Password Reset Request</h2>
            <p>You requested a password reset. Click the link below to set a new password:</p>
            <p><a href="https://your-domain.com/reset-password?token={token}">Reset Password</a></p>
            <p>This link expires in 1 hour. If you did not request this, ignore this email.</p>
            """;

        await SendAsync(email, subject, body, ct);
    }

    private async Task SendAsync(string toEmail, string subject, string htmlBody, CancellationToken ct)
    {
        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_fromAddress, _fromName);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_smtpHost, _smtpPort);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_fromAddress, _password);

            await client.SendMailAsync(message, ct);

            _logger.LogInformation("Email sent to {Email} with subject '{Subject}'", toEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
            throw;
        }
    }
}
