using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class WalletService: CrudService<WalletDto, Wallet>, IWalletService
    {
        private readonly ICrudRepository<Wallet> _walletRepository;
        public WalletService(ICrudRepository<Wallet> crudRepository, IMapper mapper, ICrudRepository<Wallet> walletRepository) : base(crudRepository, mapper)
        {
            _walletRepository = walletRepository;
        }


        public Result<WalletDto> AddAC(WalletDto walletDto)
        {
            Wallet wallet = _walletRepository.Get(walletDto.Id);
            if (wallet != null)
            {
                wallet.AC += walletDto.AC;
                _walletRepository.Update(wallet);
                return MapToDto(wallet);
            }
            return Result.Fail(FailureCode.NotFound).WithError("Wallet not found.");
        }


    }
}
