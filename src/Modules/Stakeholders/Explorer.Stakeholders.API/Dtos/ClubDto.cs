namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public long OwnerId { get; set; }
        public List<long> MemberIds { get; set; }
    }
}
