using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly PaymentsContext _dbContext;
        public WalletRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Wallet GetWalletByUserId(int userId)
        {
            return _dbContext.Wallets.FirstOrDefault(sc => sc.UserId == userId);
        }
    }
}
