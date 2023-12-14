namespace Explorer.Stakeholders.API.Dtos
{
    public class TokenDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Value { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
