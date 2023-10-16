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
        public int Difficulty { get; private set; }
        public int WalkingRating { get; private set; }
        public int BicycleRating { get; private set; }
        public int CarRating { get; private set; }
        public int BoatRating { get; private set; }
        public List<string>? Tags { get; private set; }

        public TourPreference(int difficulty, int walkingRating, int bicycleRating, int carRating, int boatRating, List<string>? tags)
        {
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

        }
    }
}
