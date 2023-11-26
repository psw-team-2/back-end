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
        public int AC { get; init; }
        public Wallet(long userId, string username, int aC)
        {
            UserId = userId;
            Username = username;
            AC = aC;
        }
    }
}
