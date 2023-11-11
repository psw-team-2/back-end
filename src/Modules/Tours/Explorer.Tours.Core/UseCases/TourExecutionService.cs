using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
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
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        public TourExecutionService(ITourExecutionRepository tourExecutionRepository, ICrudRepository<TourExecution> repository, IMapper mapper) : base(repository, mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
        }
        public Result StartTour(TourExecutionDto dto)
        {
            TourExecution tourExecution = new TourExecution() {TouristId = dto.TouristId, TourId = dto.TourId, StartTime = dto.StartTime, 
                                                               EndTime = dto.EndTime, Completed = dto.Completed, Abandoned = dto.Abandoned,
                                                               CurrentLatitude = dto.CurrentLatitude, CurrentLongitude = dto.CurrentLongitude};
            tourExecution = _tourExecutionRepository.Create(tourExecution);
            
            return Result.Ok();
        }

        public Result<TourExecutionDto> CompleteTour(int tourExecutionId)
        {
            TourExecution tourExecution = _tourExecutionRepository.Get(tourExecutionId);
            tourExecution.Completed = true;
            tourExecution.EndTime = DateTime.UtcNow;
            _tourExecutionRepository.Update(tourExecution);
            return Result.Ok();
        }

        public Result<TourExecutionDto> AbandonTour(int tourExecutionId)
        {
            TourExecution tourExecution = _tourExecutionRepository.Get(tourExecutionId);
            tourExecution.Abandoned = true;
            tourExecution.EndTime = DateTime.UtcNow;
            _tourExecutionRepository.Update(tourExecution);
            return Result.Ok();
        }
    }

}
