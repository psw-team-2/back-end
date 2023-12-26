using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class GiftCardService : CrudService<GiftcardDto, Giftcard>, IGiftCardService
    {
        private readonly IWalletService _walletService;
        private readonly IEmailService _emailService;
        public GiftCardService(ICrudRepository<Giftcard> crudRepository, IMapper mapper, IWalletService walletService, IEmailService emailService) : base(crudRepository, mapper)
        {
            _walletService = walletService;
            _emailService = emailService;
        }

        public override Result<GiftcardDto> Create(GiftcardDto giftcardDto)
        {
            var sendACResult = _walletService.SendAC(giftcardDto.AC, giftcardDto.Receiver, giftcardDto.SenderId);

            if (sendACResult.IsSuccess)
            {
                var createResult = base.Create(giftcardDto);

                if (createResult.IsSuccess)
                {
                    var createdGiftcardDto = createResult.Value;
                    _emailService.SendEmailToTourist(createdGiftcardDto);
                    return Result.Ok(createdGiftcardDto);
                }
                else
                {
                    // Rollback AC transaction if gift card creation fails
                    // Implement appropriate rollback logic here
                    // ...
                    return Result.Fail<GiftcardDto>("Failed to create gift card");
                }
            }
            else
            {
                // Handle insufficient AC or other failures from SendAC
                return Result.Fail<GiftcardDto>("Failed to send AC or insufficient balance");
            }
        }



    }
}
