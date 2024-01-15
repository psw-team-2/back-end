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
using System.Net;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using BlogStatusDto = Explorer.Blog.API.Dtos.BlogStatus;
using BlogStatus = Explorer.Blog.Core.Domain.Blog.BlogStatus;
using BlogCategoryDto = Explorer.Blog.API.Dtos.BlogCategory;
using BlogCategory = Explorer.Blog.Core.Domain.Blog.BlogCategory;

namespace Explorer.Blog.Core.UseCases
{
    public class UserBlogService : CrudService<UserBlogDto, UserBlog>, IUserBlogService
    {
        private readonly IUserBlogRepository _blogRepository;
        private readonly IBlogCommentService _blogCommentService;
        private readonly IEquipmentService _equipmentService;
        private readonly ICheckPointService _checkpointService;

        public UserBlogService(IUserBlogRepository blogRepository,  IBlogCommentService blogCommentService, ICrudRepository<UserBlog> repository, 
            IEquipmentService equipmentService, ICheckPointService checkPointService ,IMapper mapper) : base(repository, mapper) 
        {
            _blogRepository = blogRepository;
            _blogCommentService = blogCommentService;
            _equipmentService = equipmentService;
            _checkpointService = checkPointService;
        }

        public override Result<UserBlogDto> Create(UserBlogDto blogDto)
        {
            blogDto.CreationTime = DateTime.UtcNow;

            if (blogDto.TourReport != null)
            {
                if (blogDto.TourReport.EndTime < blogDto.TourReport.StartTime)
                {
                    return Result.Fail(FailureCode.InvalidArgument);
                }

                if (!blogDto.TourReport.CheckpointsVisited.Any() || blogDto.TourReport.CheckpointsVisited == null)
                {
                    return Result.Fail(FailureCode.InvalidArgument);
                }

            }

            return base.Create(blogDto);
        }

        public List<UserBlogDto> GetByUserId(int userId)
        {
            var blogs = _blogRepository.GetByUserId(userId);

            var blogDtos = blogs.Select(blog => new UserBlogDto
            {
                Id = (int)blog.Id,
                UserId = blog.UserId,
                Username = blog.Username,
                Title = blog.Title,
                Description = blog.Description,
                CreationTime = blog.CreationTime,
                Status = (API.Dtos.BlogStatus)blog.Status,
                Image = blog.Image,
                Category = (API.Dtos.BlogCategory)blog.Category
               
            }).ToList();

            return blogDtos;
        }

        public Result DeleteAll(int blogId)
        {
            var result = _blogCommentService.DeleteCommentsByBlogId(blogId);

            if (result.IsSuccess)
            {
               
                var deleteResult = Delete(blogId);

                if (deleteResult.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    
                    return deleteResult;
                }
            }
            else
            {
                var res = Delete(blogId);
                if (res.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {

                    return res;
                }
                return Result.Ok();
            }
        }



        public Result AddRating(RatingDto ratingDto)
        {
            Rating rating = new Rating(ratingDto.isUpvote, ratingDto.UserId, DateTime.UtcNow);

            UserBlog userBlog = _blogRepository.GetById((int)ratingDto.BlogId);
            userBlog.AddRating(rating);

            _blogRepository.Update(userBlog);
           

            return Result.Ok();

        }

        public Result<RatingCount> GetRatingsCount(int blogId)
        {
            UserBlog blog = _blogRepository.GetById(blogId);
            RatingCount count = new RatingCount { Count = blog.GetRatingsCount()};
            return count;
        }

        public Result<PagedResult<UserBlogDto>> GetByStatus(API.Dtos.BlogStatus status, int page, int pageSize)
        {
            var blogs = _blogRepository.GetByStatus((Domain.Blog.BlogStatus)status, page, pageSize);

            var blogDtos = blogs.Results.Select(blog => new UserBlogDto
            {
                Id = (int)blog.Id,
                UserId = blog.UserId,
                Username = blog.Username,
                Title = blog.Title,
                Description = blog.Description,
                CreationTime = blog.CreationTime,
                Status = (API.Dtos.BlogStatus)blog.Status,
                Image = blog.Image,
                Category = (API.Dtos.BlogCategory)blog.Category,

            }).ToList();

            return new PagedResult<UserBlogDto>(blogDtos, blogs.TotalCount);
        }

        public  Result AddComment(BlogCommentDto blogCommentDto)
        {
            BlogComment comment = new BlogComment(blogCommentDto.UserId, blogCommentDto.Username, blogCommentDto.BlogId, blogCommentDto.Text, DateTime.UtcNow, DateTime.UtcNow);
            var blog = _blogRepository.GetById((int)blogCommentDto.BlogId);
            blog.AddComment(comment);
            _blogRepository.Update(blog);

            return Result.Ok();
        }

        public Result<UserBlogTourReportDto> CreateWithTourReport(UserBlogTourDto dto)
        {
            return null;
        }

        public Result<PagedResult<EquipmentDto>> GetEquipmentByUserBlog(int blogId, int page, int pageSize)
        {
            try
            {
                var blog = _blogRepository.GetById(blogId);
                var blogDto = MapToDto(blog);
                if (blogDto != null)
                {
                    if (blogDto.TourReport != null && blogDto.TourReport.Equipment != null)
                    {
                        var equipment = _equipmentService.GetByIds(blogDto.TourReport.Equipment, page, pageSize);
                        return equipment;
                    }
                    else
                    {
                        return Result.Fail(FailureCode.InvalidArgument);
                    }
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<PagedResult<CheckPointDto>> GetCheckpointstByUserBlog(int blogId, int page, int pageSize)
        {
            try
            {
                var blog = _blogRepository.GetById(blogId);
                var blogDto = MapToDto(blog);
                if(blogDto != null){
                    if (blogDto.TourReport != null)
                    {
                        var checkpoints = _checkpointService.GetCheckPointByCheckpointVisitedIds(blogDto.TourReport.CheckpointsVisited);
                        return checkpoints;
                    }
                    return Result.Fail(FailureCode.InvalidArgument);
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

    }
}
