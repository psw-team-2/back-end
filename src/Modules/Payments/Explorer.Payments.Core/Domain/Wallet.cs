using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Wallet: Entity
    {
        public long UserId { get; init; }

        public int AC { get; init; }
        public Wallet(long userId, int aC)
        {
            UserId = userId;
            AC = aC;
        }
    }
}
