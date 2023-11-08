namespace Explorer.Tours.API.Dtos
{
    public class PublicRequestDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int AuthorId { get; set; }
        public string Comment { get; set; }
        public bool IsCheckPoint { get; set; }
        public bool IsNotified{ get; set; }
    }
}
