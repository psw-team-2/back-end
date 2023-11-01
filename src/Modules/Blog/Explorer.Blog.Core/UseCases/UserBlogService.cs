using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class UserBlogService : CrudService<UserBlogDto, UserBlog>, IUserBlogService
    {
        private readonly IUserBlogRepository _blogRepository;
        public UserBlogService(IUserBlogRepository blogRepository, ICrudRepository<UserBlog> repository, IMapper mapper) : base(repository, mapper) 
        {
            _blogRepository = blogRepository;
        }

        public Result<UserBlogDto> Create(UserBlogDto blogDto)
        {
            blogDto.CreationTime = DateTime.UtcNow;
            return base.Create(blogDto);
        }

        public List<UserBlogDto> GetByUserId(int userId)
        {
            var blogs = _blogRepository.GetByUserId(userId);

            var blogDtos = blogs.Select(blog => new UserBlogDto
            {
                Id = (int)blog.Id,
                UserId = blog.UserId,
                Title = blog.Title,
                Description = blog.Description,
                CreationTime = blog.CreationTime,
                Status = (API.Dtos.BlogStatus)blog.Status,
                Image = blog.Image
               
            }).ToList();

            return blogDtos;
        }

    }
}
