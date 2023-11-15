using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Price : ValueObject
    {
        public double Amount { get; }

        public Price()
        {

        }


        [JsonConstructor]
        public Price(double amount)
        {
            Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }

        public Price ApplyDiscount(double discount)
        {
            if (discount < 0 || discount > 1)
            {
                throw new ArgumentException("Discount should be between 0 and 1.");
            }

            double discountedAmount = Amount - (Amount * discount);
            return new Price(discountedAmount);
        }
    }
}
