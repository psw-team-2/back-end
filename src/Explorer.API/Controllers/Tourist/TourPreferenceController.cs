using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;


    [Route("api/tourist/preference")]
    public class TourPreferenceController: BaseApiController
    {
        private readonly ITourPreferenceService _tourPreferenceService;

    public TourPreferenceController(ITourPreferenceService tourPreferenceService)
    {
        _tourPreferenceService = tourPreferenceService;
    }

    [HttpGet]
    public ActionResult<PagedResult<TourPreferenceDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _tourPreferenceService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }
}
