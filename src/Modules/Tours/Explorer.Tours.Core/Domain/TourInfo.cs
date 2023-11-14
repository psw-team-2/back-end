using Explorer.BuildingBlocks.Core.Domain;
using System.Security.Principal;
using System.Transactions;

namespace Explorer.Tours.Core.Domain
{
    public enum AccountStatus
    {
        DRAFT, PUBLISHED, ARCHIVED
    }

    public class TourInfo : Entity
    {
        public String Name { get; init; }
        public String Description { get; init; }
        public AccountStatus Status { get; init; }
        public int Difficulty { get; init;}
        public Price Price { get; init; }
        public String? Tags { get; init; }
        public bool IsDeleted { get; init; } = false;

        public TourInfo(String name, String description, AccountStatus status, int difficulty, Price price, String? tags)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            if (double.IsNegative(price)) throw new ArgumentException("Invalid Name.");
            //if (status!=AccountStatus.DRAFT || status!=AccountStatus.PUBLISHED || status!=AccountStatus.ARCHIVED) throw new ArgumentException("Invalid account status.");
            //if (difficulty!=1 || difficulty != 2 || difficulty != 3 || difficulty != 4 || difficulty != 5) throw new ArgumentException("Invalid difficulty.");
       
            Name = name;
            Description = description;
            Status = status;
            Difficulty = difficulty;
            Price = price;
            Tags = tags;

        
        }
    }
}
