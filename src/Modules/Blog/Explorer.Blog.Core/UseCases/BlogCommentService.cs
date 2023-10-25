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

        public override Result<BlogCommentDto> Create(BlogCommentDto blogCommentDto)
        {
            blogCommentDto.CreationTime = DateTime.UtcNow;
            blogCommentDto.LastModification = DateTime.UtcNow;
            blogCommentDto.BlogId = 1;
            return base.Create(blogCommentDto);
        }

        public override Result<BlogCommentDto> Update(BlogCommentDto blogCommentDto)
        {
            Console.WriteLine(blogCommentDto) ;
            blogCommentDto.LastModification = DateTime.UtcNow;
            Console.WriteLine(blogCommentDto);
            return base.Update(blogCommentDto);
        }

    }
}
