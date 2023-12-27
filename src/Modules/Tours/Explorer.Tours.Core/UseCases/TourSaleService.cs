using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
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
    public class TourSaleService : CrudService<TourSaleDto, TourSale>, ITourSaleService
    {
        private readonly ICrudRepository<TourSale> _tourSaleRepository;

        public TourSaleService(ICrudRepository<TourSale> tourSaleRepository, ICrudRepository<TourSale> repository, IMapper mapper) : base(repository, mapper)
        {
            _tourSaleRepository = tourSaleRepository;
        }

        public Result Delete(int id, int authorId)
        {
            TourSale sale;
            try
            {
                sale = _tourSaleRepository.Get(id);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            if (!sale.IsCreatedByUser(authorId))
                return Result.Fail(FailureCode.Forbidden);
            try
            {
                CrudRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
