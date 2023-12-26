using Castle.Core.Smtp;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;


namespace Explorer.Stakeholders.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;

        private readonly ITourService _tourService;
        public EmailService(IUserRepository userRepository, ITourService tourService)
        {
            _userRepository = userRepository;
            _tourService = tourService;
        }

        public void SendEmailToTourist(GiftcardDto giftcardDto)
        {
            string touristEmail = _userRepository.GetTouristEmail(giftcardDto.Receiver);
            User reciever = _userRepository.Get(giftcardDto.Receiver);
            Result<TourDto> tour = _tourService.Get(giftcardDto.RecommendedTour);

            string tourLink = $"http://localhost:4200/tour/{tour.Value.Id}"; // Promenite link prema vašoj aplikaciji

            string subject = "New Gift card";
            string body = $"Hello {reciever.Username},\n\n" +
                $"You have received a new gift card! Details are as follows:\n" +
                $"Amount: {giftcardDto.AC} AC\n" +
                $"Message: {giftcardDto.Note}\n" +
                $"Sender: {giftcardDto.Sender}\n" +
                $"Recommended Tour: {tour.Value.Name}\n" +
                $"Click here to view the tour {tourLink}\n\n" +
                $"Thank you for using our services,\n " +
                $"Explorer team \U0001F334";




            SendEmail(touristEmail, subject, body);
        }

        public void SendEmailToAdmins(QuestionDto questionDto)
        {
            List<string> adminEmails = _userRepository.GetAdminEmails();

            foreach (var adminEmail in adminEmails)
            {
                string subject = "New Question";
                string body = $"Hello,\n\nYou received a new question: {questionDto.Text}\n"  + "Give a response here: http://localhost:4200/questions-overview\n\n\nExplorer Team\nTelephone: +381 9752435";

                SendEmail(adminEmail, subject, body);
            }
        }

        public void SendEmailToUser(AnswerDto answer)
        {
            string userEmail = _userRepository.GetUserEmail(answer.TouristId);
        
            string subject = "New Answer";
            string body = $"Hello,\n\nYou received an answer: {answer.Text}\n" + "Look for it here: http://localhost:4200/faq\n\n\nExplorer Team\nTelephone: +381 9752435";

            SendEmail(userEmail, subject, body);


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
