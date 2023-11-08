using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.Core.Domain.Blog;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogCommentRepository
    {

        List<BlogComment> GetCommentsByBlogId(int blogId);


    }
}
