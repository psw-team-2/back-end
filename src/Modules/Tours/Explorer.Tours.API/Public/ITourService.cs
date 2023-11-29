using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Get(int id);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(int id);

        public Result<List<TourDto>> GetToursListByAuthor(long authorId, int page, int pageSize);
        
        Result<TourDto> AddCheckPoint(TourDto tour, int checkPoint);
        Result<TourDto> DeleteCheckPoint(TourDto tour, int checkPointId);
        Result<TourDto> AddEquipmentToTour(TourDto tour, int equipmentId);
        Result<TourDto> RemoveEquipmentFromTour(TourDto tour, int equipmentId);
        Result<AverageGradeDto> GetAverageGradeForTour(int tourId);
        List<TourReviewDto> GetByTourId(int tourId);

        public Result<TourDto> PublishTour(TourDto tour);
        public Result<TourDto> ArchiveTour(TourDto tour);

        List<TourBundleDto> GetToursByAuthorId(int authorId);


    }
}
