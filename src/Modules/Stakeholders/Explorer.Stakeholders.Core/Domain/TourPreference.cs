using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TourPreference : Entity
    {
        public int TouristId { get; private set; }
        public int Difficulty { get; private set; }
        public int WalkingRating { get; private set; }
        public int BicycleRating { get; private set; }
        public int CarRating { get; private set; }
        public int BoatRating { get; private set; }
        public List<string>? Tags { get; private set; }

        public TourPreference(int touristId, int difficulty, int walkingRating, int bicycleRating, int carRating, int boatRating, List<string>? tags)
        {
            TouristId = touristId; 
            Difficulty = difficulty;
            WalkingRating = walkingRating;
            BicycleRating = bicycleRating;
            CarRating = carRating;
            BoatRating = boatRating;
            Tags = tags;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");    
            if (Difficulty < 1 || Difficulty > 5) throw new ArgumentException("Invalid Difficulty");
            if (WalkingRating < 1 || WalkingRating > 5) throw new ArgumentException("Invalid WalkingRating");
            if (BoatRating < 1 || BoatRating > 5) throw new ArgumentException("Invalid BoatRating");
            if (CarRating < 1 || CarRating > 5) throw new ArgumentException("Invalid CarRating");
            if (BicycleRating < 1 || BicycleRating > 5) throw new ArgumentException("Invalid BicycleRating");
        }
    }
}
