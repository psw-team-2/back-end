﻿using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain.Blog;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<UserBlogTourReportDto, UserBlogTourReport>().ReverseMap();
        CreateMap<UserBlogDto, UserBlog>().ReverseMap();
        CreateMap<BlogCommentDto,BlogComment>().ReverseMap();
    }
}