using Explorer.Tours.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public interface IEmailService
    {
        void SendEmailToTourist(GiftcardDto giftcardDto);
        void SendEmailToAdmins(QuestionDto questionDto);
        void SendEmail(string toEmail, string subject, string body);
        void SendEmailToUser(AnswerDto answer);
    }
}
