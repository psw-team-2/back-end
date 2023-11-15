using Explorer.Blog.Core.Domain.Blog;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly BlogContext _dbContext;
        public BlogCommentRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BlogComment> GetCommentsByBlogId(int blogId)
        {
            return _dbContext.Comments.Where(c => c.BlogId == blogId).ToList();
        }


    }
}
