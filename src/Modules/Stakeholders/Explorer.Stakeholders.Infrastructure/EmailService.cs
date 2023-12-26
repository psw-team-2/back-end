using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;
        public EmailService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void SendEmailToAdmins(QuestionDto questionDto)
        {
            List<string> adminEmails = _userRepository.GetAdminEmails();

            foreach (var adminEmail in adminEmails)
            {
                string subject = "New Question";
                string body = $"You revieved a new question:\n\n{questionDto.Text}" ;

                SendEmail(adminEmail, subject, body);
            }
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            string fromEmail = "psw2023grupa2@gmail.com";
            string emailPassword = "iqjm gqhi dgvs nyjc";

            MailMessage mailMessage = new MailMessage(fromEmail, toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Credentials = new NetworkCredential(fromEmail, emailPassword);
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email to {toEmail}: {ex.Message}");
            }
        }
    }
}
