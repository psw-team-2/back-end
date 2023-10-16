
namespace Explorer.Blog.API.Dtos
{
    public class UserBlogDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public string Image { get; set; }
        public BlogStatus Status { get; set; }
    }

    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }
}
