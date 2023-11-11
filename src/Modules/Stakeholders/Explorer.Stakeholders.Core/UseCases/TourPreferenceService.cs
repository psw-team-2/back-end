using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    
    public class TourPreferenceService
    {
        /*
        public TourPreferenceService(ICrudRepository<TourPreference> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TourPreferenceDto> GetByTouristId(int touristId)
        {
            try
            {
                var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);
                var preferences = pagedResult.Results.Where(p => p.TouristId == touristId).FirstOrDefault();

                if (preferences != null)
                {
                    return MapToDto(preferences);
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Tour preference not found for the specified tourist.");
                }
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        */
    }

}
