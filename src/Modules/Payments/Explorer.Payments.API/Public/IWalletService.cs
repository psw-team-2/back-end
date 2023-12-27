using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IWalletService
    {
        Result<PagedResult<WalletDto>> GetPaged(int page, int pageSize);
        Result<WalletDto> Get(int id);
        Result<WalletDto> Create(WalletDto walletDto);
        Result<WalletDto> AddAC(WalletDto walletDto);
        Result<WalletDto> GetWalletByUserId(int userId);
        Result<int> SendAC(int AC, int receiverWalletId, int senderWalletId);

    }
}
