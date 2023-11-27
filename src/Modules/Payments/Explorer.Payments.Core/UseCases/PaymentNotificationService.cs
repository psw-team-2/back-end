using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class PaymentNotificationService : CrudService<PaymentNotificationDto, PaymentNotification>, IPaymentNotificationService
    {
        private readonly ICrudRepository<PaymentNotification> _paymentNotificationRepository;
        private readonly IPaymentNotificationRepository _paymentNotification;
        private readonly IMapper _mapper;
        public PaymentNotificationService(ICrudRepository<PaymentNotification> crudRepository, IMapper mapper, IPaymentNotificationRepository paymentNotificationRepository) : base(crudRepository, mapper)
        {
            _paymentNotificationRepository = crudRepository;
            _paymentNotification = paymentNotificationRepository;
            _mapper = mapper;
        }

        public Result<PagedResult<PaymentNotificationDto>> GetUnreadPaymentNotifications(int page, int pageSize, long profileId)
        {
            var unreadNotifications = _paymentNotification.GetAll()
                .Where(notification => notification.UserId == profileId && notification.Status == 0)
                .ToList();

            var unreadNotificationsDtos = _mapper.Map<List<PaymentNotificationDto>>(unreadNotifications);

            var pagedResult = Result.Ok(new PagedResult<PaymentNotificationDto>(unreadNotificationsDtos, unreadNotificationsDtos.Count));

            return pagedResult;

        }

    }
}
