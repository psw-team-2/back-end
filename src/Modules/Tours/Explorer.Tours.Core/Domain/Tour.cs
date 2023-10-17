using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : TourInfo
    {
        public List<int>? Equipments { get; init; }

        //public List <CheckPoint> Checkpoints { get; init; }

        //public List <Object> Objects { get; init;

       // public bool IsDeleted { get; set; } = false;

        public Tour(String name, String description, AccountStatus status,int difficulty, double price, List<String>? tags, List<int>? equipments) : base(name,description, status,difficulty, price, tags)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            if(difficulty != 1 || difficulty != 2 || difficulty != 3 || difficulty != 4 || difficulty != 5) throw new ArgumentException("Invalid difficulty.");

            Name = name;
            Description = description;
            Difficulty = difficulty;
            Status = AccountStatus.DRAFT;
            Price = 0;
            Tags = tags;
            Equipments=equipments;
            //Checkpoints = checkpoints;
            //Objects = objects;



        }

    }
}
