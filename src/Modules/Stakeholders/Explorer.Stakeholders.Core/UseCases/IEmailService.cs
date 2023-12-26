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
        void SendEmailToAdmins(QuestionDto questionDto);

        void SendEmail(string toEmail, string subject, string body);
    }
}
