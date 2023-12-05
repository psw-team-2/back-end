using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class PurchaseReportRepository : IPurchaseReportRepository
    {
        private readonly PaymentsContext _dbContext;
        public PurchaseReportRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PurchaseReport> GetPurchaseReportsByTouristId(int touristId)
        {
            return _dbContext.PurchaseReports.Where(r => r.UserId == touristId).ToList();
        }
    }
}
