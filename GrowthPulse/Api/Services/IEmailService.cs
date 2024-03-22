using Api.Helpers;

namespace Api.Services;

public interface IEmailService
{
    public Task<Result> SendEmail(string emailRecipient, string subject, string body);
}