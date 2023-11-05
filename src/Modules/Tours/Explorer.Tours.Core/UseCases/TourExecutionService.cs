using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
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
        public TourExecutionService(ICrudRepository<TourExecution> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        { }

        public Result<TourExecutionDto> StartTour(int touristId, int tourId, double startLatitude, double startLongitude)
        {
            try
            {
                var tourExecution = new TourExecution
                {
                    TouristId = touristId,
                    TourId = tourId,
                    StartTime = DateTime.UtcNow,
                    CurrentLatitude = startLatitude,
                    CurrentLongitude = startLongitude
                };

                var createdTourExecution = Create(tourExecution);
                var tourExecutionDto = Mapper.Map<TourExecutionDto>(createdTourExecution);

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourExecutionDto>(ex.Message);
            }
        }

        public Result<TourExecutionDto> CompleteTour(int tourExecutionId, double endLatitude, double endLongitude)
        {
            try
            {
                var tourExecution = CrudRepository.Get(tourExecutionId);

                if (tourExecution == null)
                    return Result.Fail<TourExecutionDto>("Tour execution not found.");

                if (tourExecution.Completed || tourExecution.Abandoned)
                    return Result.Fail<TourExecutionDto>("Tour execution is already completed or abandoned.");

                tourExecution.EndTime = DateTime.UtcNow;
                tourExecution.CurrentLatitude = endLatitude;
                tourExecution.CurrentLongitude = endLongitude;
                tourExecution.Completed = true;

                Update(tourExecution);

                var tourExecutionDto = Mapper.Map<TourExecutionDto>(tourExecution);

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourExecutionDto>(ex.Message);
            }
        }

        public Result<TourExecutionDto> AbandonTour(int tourExecutionId, double abandonLatitude, double abandonLongitude)
        {
            try
            {
                var tourExecution = CrudRepository.Get(tourExecutionId);

                if (tourExecution == null)
                    return Result.Fail<TourExecutionDto>("Tour execution not found.");

                if (tourExecution.Completed || tourExecution.Abandoned)
                    return Result.Fail<TourExecutionDto>("Tour execution is already completed or abandoned.");

                tourExecution.EndTime = DateTime.UtcNow;
                tourExecution.CurrentLatitude = abandonLatitude;
                tourExecution.CurrentLongitude = abandonLongitude;
                tourExecution.Abandoned = true;

                Update(tourExecution);

                var tourExecutionDto = Mapper.Map<TourExecutionDto>(tourExecution);

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourExecutionDto>(ex.Message);
            }
        }
    }

}
