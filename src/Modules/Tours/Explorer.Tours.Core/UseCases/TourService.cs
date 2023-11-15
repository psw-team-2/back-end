using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;


namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        private readonly IUserAccountAdministrationService _userAccountService;

        public TourService(ICrudRepository<Tour> repository, IMapper mapper, IUserAccountAdministrationService userAccountService) : base(repository, mapper)
        {
            _userAccountService = userAccountService;


        }

        public Result<List<TourDto>> GetToursListByAuthor(long authorId, int page, int pageSize)
        {

            var userResult = _userAccountService.GetUserById((int)authorId);

            if (userResult.IsSuccess && userResult.Value != null)
            {
                var tours = base.GetPaged(page, pageSize);
                var authorsTours = tours.Value.Results.Where(tour => tour.AuthorId == authorId).ToList();

                return Result.Ok(authorsTours);

            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError("Failed to retrieve author information");
            }
        }

        public Result<TourDto> AddCheckPoint(TourDto tour, int checkPointId) {

            if (tour != null) 
            { 
                tour.CheckPoints.Add(checkPointId);
                Update(tour);
            }
            return tour;
        }

        public Result<TourDto> DeleteCheckPoint(TourDto tour, int checkPointId)
        {

            if (tour != null)
            {
                tour.CheckPoints.Remove(checkPointId);
                Update(tour);
            }
            return tour;
        }


        public Result<TourDto> AddEquipmentToTour(TourDto tour, int equipmentId)
        {
            if(tour != null)
            {
                tour.Equipments.Add(equipmentId);
                Update(tour);
            }
            return tour;
        }

        public Result<TourDto> RemoveEquipmentFromTour(TourDto tour, int equipmentId)
        {
            if (tour != null)
            {
                tour.Equipments.Remove(equipmentId);
                Update(tour);
            }
            return tour;
        }


    }
}
