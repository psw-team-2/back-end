using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using MailKit.Net.Smtp;
using MimeKit;

public static class EmailService
{
    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string UserName = "dmjmisa@gmail.com";
    private const string Password = "lqrk rhil pxyb scai";

    public static Result SendVerificationEmail(string toAddress, string userName, string token)
    {
        try
        {
            var subject = "Verify Your Email - Your App";
            var body = $"Hello {userName},\n\nPlease click the following link to verify your email: " +
                       $"http://localhost:4200/verify/{token}";

            SendEmail(toAddress, subject, body);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public static Result SendRecoveryEmail(string toAddress, string token)
    {
        try
        {
            var subject = "Recover Your Password - Your App";
            var body = $"Please click the following link to recover your password: " +
                       $"http://localhost:4200/recover/{token}";

            SendEmail(toAddress, subject, body);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private static void SendEmail(string toAddress, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("PSW-Tim-2", UserName));
        message.To.Add(new MailboxAddress("", toAddress));
        message.Subject = subject;

        var builder = new BodyBuilder();
        builder.TextBody = body;
        message.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect(SmtpServer, SmtpPort, false);
            client.Authenticate(UserName, Password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}