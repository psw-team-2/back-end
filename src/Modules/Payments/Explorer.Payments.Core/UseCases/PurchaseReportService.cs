using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class PurchaseReportService : CrudService<PurchaseReportDto, PurchaseReport>, IPurchaseReportService
    {
        public readonly IPurchaseReportRepository _purchaseReportRepository;
        public PurchaseReportService(ICrudRepository<PurchaseReport> crudRepository, IPurchaseReportRepository purchaseReportRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _purchaseReportRepository = purchaseReportRepository;
        }

        public Result Create(List<OrderItemDto> orderItems, int userId)
        {
            foreach (OrderItemDto item in orderItems)
            {
                PurchaseReport purchaseReport = new PurchaseReport(userId, item.ItemId, item.Price, DateTime.UtcNow);
                base.Create(MapToDto(purchaseReport));
            }

            return Result.Ok();
        }

        public List<PurchaseReportDto> GetPurchaseReportsByTouristId(int touristId)
        {
            List<PurchaseReport> reports = _purchaseReportRepository.GetPurchaseReportsByTouristId(touristId);

            var purchaseReportDto = reports.Select(report => new PurchaseReportDto
            {
                Id = (int)report.Id,
                UserId = report.UserId,
                TourId = report.TourId,
                AdventureCoin = report.AdventureCoin,
                PurchaseDate = report.PurchaseDate,
                
            }).ToList();

            return purchaseReportDto;
        }
    }
}

