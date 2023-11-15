using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Object : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsPublic { get; set; }

        public ObjectType.Category Category { get; set; }

        public Object(string name, string description, string image, bool isPublic, ObjectType.Category category)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Name.");
            }

           
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Invalid Description.");
            }

            if (string.IsNullOrWhiteSpace(image))
            {
                throw new ArgumentException("Invalid Image.");
            }

            Name = name;
            Description = description;
            Image = image;
            IsPublic = isPublic;
            Category = category;
        }


    }
}
