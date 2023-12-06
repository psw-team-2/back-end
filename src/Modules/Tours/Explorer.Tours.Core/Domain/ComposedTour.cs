using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class ComposedTour:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountStatus Status { get; set; }
        public List<long>? Equipment { get; init; }
        public List<int>? ToursId { get; set; }
        public List<long>? Checkpoints { get; init; }
        public int Difficulty { get; set; }
        public List<long>? Objects { get; init; }
        public List<string>? Tags { get; set; }
        public double FootTime { get; init; }
        public double BicycleTime { get; init; }
        public double CarTime { get; init; }
        public double TotalLength { get; init; }
        public long AuthorId { get; set; }
        public DateTime PublishTime { get; init; }

        public ComposedTour(string name, string description, AccountStatus status, int difficulty,  List<long> equipment,
        List<int> toursId, List<long> checkpoints, List<long>? objects, List<string>? tags, double footTime, double bicycleTime,
        double carTime, double totalLength, long authorId, DateTime publishTime)
        {
            Name = name;
            Description = description;
            Status = status;
            Equipment = equipment;
            ToursId = toursId;
            Checkpoints = checkpoints;
            Difficulty = difficulty;
            Objects = objects;
            Tags = tags;
            FootTime = footTime;
            BicycleTime = bicycleTime;
            CarTime = carTime;
            TotalLength = totalLength;
            AuthorId = authorId;
            PublishTime = publishTime;
        }
    }
}
