using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class PaymentNotificationRepository : IPaymentNotificationRepository
    {
        private readonly PaymentsContext _dbContext;

        public PaymentNotificationRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PaymentNotification> GetAll()
        {
            return _dbContext.PaymentNotifications.ToList();
        }
    }
}
