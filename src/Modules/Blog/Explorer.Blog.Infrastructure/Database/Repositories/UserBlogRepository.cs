using Explorer.Blog.Core.Domain.Blog;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
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

        /*
        public UserBlog GetWithComments(int blogId)
        {
            var blog = _dbContext.Blogs
                     .Where(b => b.Id == blogId).Include(b => b.BlogComments);
            if (blog == null) throw new KeyNotFoundException("Not found: " + blogId);
            return (UserBlog)blog;
        }*/


        public UserBlog GetById(int blogId)
        {
            return _dbContext.Blogs
                            .FirstOrDefault(b => b.Id == blogId);
        }


        public UserBlog Update(UserBlog blog)
        {
            try
            {
                _dbContext.Update(blog);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return (UserBlog)blog;
        }

        public List<UserBlog> GetByStatus(BlogStatus status)
        {
            return _dbContext.Blogs
                .Where(b => b.Status == status)
                .ToList();
        }
    }
}
