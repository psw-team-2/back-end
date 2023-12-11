using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : TourInfo
    {

        public List<long>? Equipment { get; init; }

        public List<long>? Checkpoints { get; init; }

        public List<long>? Objects { get; init; }
        public List<TourReview>? TourReviews { get; init;}

        //public bool IsDeleted { get; set; } = false;

        public double FootTime { get; init; } 
        public double BicycleTime { get; init; } 
        public double CarTime { get; init; }
        public double TotalLength { get; init; }
        public long AuthorId { get; set; }
        public DateTime PublishTime { get; init; }

        public string Image { get; init; }

        public Tour(String name, String description, AccountStatus status,int difficulty, double price, double footTime, double bicycleTime, double carTime, double totalLength, long authorId, string image) : base(name, description, status, difficulty, price)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            //if(difficulty != 1 || difficulty != 2 || difficulty != 3 || difficulty != 4 || difficulty != 5) throw new ArgumentException("Invalid difficulty.");

            Name = name;
            Description = description;
            Difficulty = difficulty;
            Status = status;
            Price = price;
            Equipment = new List<long>();
            Checkpoints = new List<long>();
            Objects = new List<long>();
            Tags = new List<string>();
            TourReviews = new List<TourReview>();
            TotalLength = totalLength;
            BicycleTime = bicycleTime;
            CarTime = carTime;
            FootTime = footTime;
            AuthorId = authorId;
            Image = image;
        }
        public double GetAverageGradeForTour()
        {
            if (TourReviews.Count == 0)
            {
                return 0; 
            }

            int totalGrade = 0;

            foreach (var grade in TourReviews)
            {  
                totalGrade += grade.Grade;
            }

            double averageGrade = (double)totalGrade / TourReviews.Count;

            return averageGrade;
        }

        public void AddTourReview(TourReview review)
        {
            if (TourReviews == null)
            {
                throw new InvalidOperationException("TourReviews list is null.");
            }

            TourReviews.Add(review);
           
        }


    }
}
