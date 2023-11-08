using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.API.Dtos
{
    public class ObjectDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsPublic { get; set; }
        public ObjectType.Category Category { get; set; }
    }
}
