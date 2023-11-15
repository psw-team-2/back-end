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

        public List<int>? Equipments { get; init; }

        public List<long>? Checkpoints { get; init; }

        public List<TourReview>? TourReviews { get; init;}

        //public List <Object> Objects { get; init;

        // public bool IsDeleted { get; set; } = false;

        public double FootTime { get; init; } 
        public double BicycleTime { get; init; } 
        public double CarTime { get; init; }
        public double TotalLength { get; init; }

        public DateTime PublishTime { get; init; }

        public Tour(String name, String description, AccountStatus status,int difficulty, double price, String? tags, double footTime, double bicycleTime, double carTime, double totalLength) : base(name,description, status,difficulty, price, tags)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            //if(difficulty != 1 || difficulty != 2 || difficulty != 3 || difficulty != 4 || difficulty != 5) throw new ArgumentException("Invalid difficulty.");

            Name = name;
            Description = description;
            Difficulty = difficulty;
            Status = AccountStatus.DRAFT;
            Price = price;
            Tags = tags;
            Equipments = new List<int>();
            Checkpoints = new List<long>();
            TourReviews = new List<TourReview>();
            TotalLength = totalLength;
            BicycleTime = bicycleTime;
            CarTime = carTime;
            FootTime = footTime;
            //Equipments=equipments;
            //Checkpoints = checkpoints;
            //Objects = objects;

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
