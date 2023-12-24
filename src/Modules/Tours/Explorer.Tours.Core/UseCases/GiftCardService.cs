using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class GiftCardService : CrudService<GiftcardDto, Giftcard>, IGiftCardService
    {
        private readonly IWalletService _walletService;
        public GiftCardService(ICrudRepository<Giftcard> crudRepository, IMapper mapper, IWalletService walletService) : base(crudRepository, mapper)
        {
            _walletService = walletService;
        }

        public override Result<GiftcardDto> Create(GiftcardDto giftcardDto)
        {
            base.Create(giftcardDto);

            _walletService.SendAC(giftcardDto.AC, giftcardDto.Receiver, giftcardDto.SenderId);

            return giftcardDto;
        }
    }
}
