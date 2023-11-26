using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
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
        public WalletService(ICrudRepository<Wallet> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<WalletDto> Create(WalletDto walletDto)
        {
            throw new NotImplementedException();
        }

        public Result<WalletDto> AddAC(WalletDto walletDto)
        {
            throw new NotImplementedException();
        }


    }
}
