using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.API.Dtos
{
    public enum AccountStatus
    {
        DRAFT, STARTED, FINISH
    }

    //public enum Difficulty
    //{
    //    EASY, MEDIUM, HARD, EXTRA_HARD
    //}
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountStatus Status { get; set; }
        public int Difficulty { get; set; }
        public double Price { get; set; }
        public List<String>? Tags { get; set; }
        public List<int>? Equipments { get; set; }

    }
}
