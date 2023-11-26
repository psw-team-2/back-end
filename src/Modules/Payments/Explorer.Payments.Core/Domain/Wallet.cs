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

        public string Username { get; init; }
        public double AC { get; set; }
        public Wallet(long userId, string username, double aC)
        {
            UserId = userId;
            Username = username;
            AC = aC;
        }
    }
}
