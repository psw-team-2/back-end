using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class AuthorReviewService : CrudService<AuthorReviewDto, AuthorReview>, IAuthorReviewService
    {
        private readonly IAuthorReviewRepository _authorReviewRepository;
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly ITourRepository _tourRepository;

        public AuthorReviewService(ICrudRepository<AuthorReview> crudRepository, IMapper mapper, IAuthorReviewRepository authorReviewRepository, ITourExecutionRepository tourExecutionRepository, ITourRepository tourRepository) : base(crudRepository, mapper)
        {
            _authorReviewRepository = authorReviewRepository;
            _tourExecutionRepository = tourExecutionRepository;
            _tourRepository = tourRepository;
        }

        public Result<AuthorReviewDto> Create(AuthorReviewDto authorReviewDto, int touristId)
        {
            var executedTours = _tourExecutionRepository.GetCompletedToursByTourist(touristId);
            var completedToursWithSameAuthor = executedTours.Count(te => IsTourByAuthor(te.TourId, authorReviewDto.AuthorId) && te.Completed);

            if (completedToursWithSameAuthor < 5)
            {
                return Result.Fail("Tourist must complete 5 tours with the same author before leaving a review");
            }

            var authorReview = new AuthorReview(authorReviewDto.Grade, authorReviewDto.Comment, authorReviewDto.AuthorId, DateTime.UtcNow, touristId);

            _authorReviewRepository.Create(authorReview);

            var authorReviewDtoResult = MapToDto(authorReview);
            return Result.Ok(authorReviewDtoResult).WithSuccess("Author review successfully created");
        }

        private bool IsTourByAuthor(int tourId, long authorId)
        {
            var tour = _tourRepository.Get(tourId);

            if (tour != null && tour.AuthorId == authorId)
            {
                return true;
            }

            return false;
        }

        public Result<PagedResult<AuthorReviewDto>> GetAuthorReviews(int authorId)
        {
            try
            {
                var authorReviews = _authorReviewRepository.GetAuthorReviews(authorId);

                var authorReviewsDto = MapToDto(authorReviews).Value;

                var pagedResult = new PagedResult<AuthorReviewDto>(authorReviewsDto, authorReviewsDto.Count);

                return Result.Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound).WithError("Author reviews not found");
            }
        }
    }
}
