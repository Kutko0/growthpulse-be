using Api.Helpers;
using FluentValidation;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public EmailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task<Result> SendEmail(string emailRecipient, string subject, string body)
    {
        // Validate that emailRecipient is a valid email address
        var validator = new EmailValidator();
        var result = await validator.ValidateAsync(new EmailModel { EmailAddress = emailRecipient });
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage);
            return Result.Fail(errors);
        }

        // Create a new MailMessage object
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("GrowthPulse", _emailConfiguration.From));
        message.To.Add(new MailboxAddress("", emailRecipient));
        message.Subject = subject;
        message.Body = new TextPart("plain", body);

        // Create a new SmtpClient object and send the message
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port,
                SecureSocketOptions.StartTls);

            // Note: only needed if the SMTP server requires authentication
            await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        return Result.Succeed();
    }

    public class EmailValidator : AbstractValidator<EmailModel>
    {
        public EmailValidator()
        {
            RuleFor(model => model.EmailAddress)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");
        }
    }

    public class EmailModel
    {
        public string EmailAddress { get; set; }
    }
}