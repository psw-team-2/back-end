using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Blog;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
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
        public BlogCommentService(IBlogCommentRepository blogCommentRepository, ICrudRepository<BlogComment> repository, IMapper mapper) : base(repository, mapper) 
        {
            _blogCommentRepository = blogCommentRepository;
        }

        public IBlogCommentRepository _blogCommentRepository;
        public override Result<BlogCommentDto> Create(BlogCommentDto blogCommentDto)
        {
            blogCommentDto.CreationTime = DateTime.UtcNow;
            blogCommentDto.LastModification = DateTime.UtcNow;
            return base.Create(blogCommentDto);
        }

        public override Result<BlogCommentDto> Update(BlogCommentDto blogCommentDto)
        {
            Console.WriteLine(blogCommentDto) ;
            blogCommentDto.LastModification = DateTime.UtcNow;
            Console.WriteLine(blogCommentDto);
            return base.Update(blogCommentDto);
        }

        public List<BlogCommentDto> GetCommentsByBlogId(int blogId)
        {
            var reviews = _blogCommentRepository.GetCommentsByBlogId(blogId);

            // Perform the necessary mapping to DTOs here.
            var reviewsDto = reviews.Select(review => new BlogCommentDto
            {
                Text = review.Text,
                CreationTime = review.CreationTime,
                LastModification = review.LastModification,
                UserId = review.UserId,
                BlogId = review.BlogId,
                Id = (int)review.Id,
                Username = review.Username
            }).ToList();

            return reviewsDto;
        }

        public Result DeleteCommentsByBlogId(int blogId) 
        {
            var comments = _blogCommentRepository.GetCommentsByBlogId(blogId);

            if (comments == null || !comments.Any())
            {
                return Result.Fail("No comments found for the specified blog ID.");
            }else
            {
                foreach (var comment in comments)
                {
                    Delete((int)comment.Id);
                }
                return Result.Ok();
            }

            

        }
    }
}
