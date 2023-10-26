namespace Explorer.Tours.API.Dtos
{
    public class EquipmentForSelectionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Selected { get; set; }

        public EquipmentForSelectionDto(long id, string name, string? description, bool selected)
        {
            Id = id;
            Name = name;
            Description = description;
            Selected = selected;
        }
    }
}
