namespace Explorer.Blog.API.Dtos
{
    public class UserBlogTourDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public BlogStatus Status { get; set; }
        public string Image { get; set; }
        public BlogCategory Category { get; set; }
        public UserBlogTourReportDto TourReport { get; set; }
    }



}