using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ApplicationReviewService : CrudService<ApplicationReviewDto, ApplicationReview>, IApplicationReviewService
    {
       public ApplicationReviewService(ICrudRepository<ApplicationReview> repository, IMapper mapper) : base(repository, mapper) { }

        private readonly IApplicationReviewRepository _applicationReviewRepository; 

        public ApplicationReviewService(IApplicationReviewRepository applicationReviewRepository, ICrudRepository<ApplicationReview> repository, IMapper mapper)
            : base(repository, mapper) 
        {
            _applicationReviewRepository = applicationReviewRepository;
        }


        public Result<ApplicationReviewDto> Create(ApplicationReviewDto applicationReviewDto)
        {
            var existingReview = _applicationReviewRepository.GetByUser(applicationReviewDto.UserId);

            applicationReviewDto.TimeStamp = DateTime.UtcNow;

            if (existingReview != null)
            {
                applicationReviewDto.Id = existingReview.Id;
                base.Delete((int)existingReview.Id);              

            }
            
            return base.Create(applicationReviewDto);
        
        }
    }
}
