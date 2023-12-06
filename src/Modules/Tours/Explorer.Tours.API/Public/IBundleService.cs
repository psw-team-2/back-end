using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IBundleService
    {
        Result<PagedResult<BundleDto>> GetPaged(int page, int pageSize);
        Result<BundleDto> Get(int id);
        Result<BundleDto> Create(BundleDto bundleDto);
        Result<BundleDto> Update(BundleDto bundleDto);
        List<BundleDto> GetBundlesByAuthorId(int authorId);
        Result<BundleDto> PublishBundle(int bundleId);
        Result<BundleDto> AddTour(BundleDto bundleDto, int tourId);
        Result<BundleDto> ArchiveBundle(int bundleId);
        Result Delete(int bundleId);
        Result<BundleDto> FinishCreatingBundle(int bundleId, double price);
        Result<BundleDto> RemoveTour(int bundleId, int tourId);
    }
}
