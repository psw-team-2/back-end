using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class UserBlogRepository: IUserBlogRepository
    {
        private readonly BlogContext _dbContext;
        public UserBlogRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserBlog> GetByUserId(int userId)
        {
            return _dbContext.Blogs
                .Where(b => b.UserId == userId)
                .ToList();
        }
    }
}
