using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.Core.Domain.Blog;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IUserBlogRepository
    {
        List<UserBlog> GetByUserId(int userId);
        //UserBlog GetWithComments(int blogId);
        UserBlog GetById(int blogId);
        UserBlog Update(UserBlog blog);
        PagedResult<UserBlog> GetByStatus(BlogStatus status, int page, int pageSize);
    }
}
