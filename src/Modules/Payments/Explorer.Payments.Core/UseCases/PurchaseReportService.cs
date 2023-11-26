using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.Core.Domain;
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
        public PurchaseReportService(ICrudRepository<PurchaseReport> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result Create(List<OrderItemDto> orderItems, int userId)
        {
            foreach (OrderItemDto item in orderItems)
            {
                PurchaseReport purchaseReport = new PurchaseReport(userId, item.TourId, item.Price);
                base.Create(MapToDto(purchaseReport));
            }

            return Result.Ok();
            //throw new NotImplementedException();
        }
    }
}
