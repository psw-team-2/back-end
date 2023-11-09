﻿using AutoMapper;
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
using FluentResults;
using FluentResults;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        public readonly ITourRepository _tourRepository;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper) 
        {
            _tourRepository = tourRepository;
        
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

        public Result<AverageGradeDto> GetAverageGradeForTour(int tourId)
        {
            var tour = _tourRepository.GetOne(tourId);
            if (tour == null)
            {
                return null;
            }
            double avg  = tour.GetAverageGradeForTour();
            AverageGradeDto dto = new AverageGradeDto { AverageGrade = avg };
            return dto;
            //return avg;
        }


      
    }
}
