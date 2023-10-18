using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.API.Dtos
{
    public class ObjectDto
    {
       



        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ObjectType.Category Category { get; set; }

        public string Image { get; set; }


    }
}
