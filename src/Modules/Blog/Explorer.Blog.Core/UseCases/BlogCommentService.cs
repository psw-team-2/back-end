using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogCommentService : CrudService<BlogCommentDto, BlogComment>, IBlogCommentService
    {
        public BlogCommentService(ICrudRepository<BlogComment> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<BlogCommentDto> Create(BlogCommentDto blogCommentDto)
        {
            blogCommentDto.CreationTime = DateTime.UtcNow;
            blogCommentDto.LastModification = DateTime.UtcNow;
            return base.Create(blogCommentDto);
        }

        public Result<BlogCommentDto> Update(BlogCommentDto blogCommentDto)
        {
            blogCommentDto.LastModification = DateTime.UtcNow;
            return base.Update(blogCommentDto);
        }

    }
}
