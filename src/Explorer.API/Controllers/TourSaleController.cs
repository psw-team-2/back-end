using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [ApiController]
    [Route("api/author/toursale")]
    //[Authorize(Policy = "authorPolicy")]
    public class TourSaleController : BaseApiController
    {
        private readonly ITourSaleService _tourSaleService;
        private readonly ITourService _tourService;
        public TourSaleController(ITourSaleService tourSaleService, ITourService tourService)
        {
            _tourSaleService = tourSaleService;
            _tourService = tourService;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<PagedResult<TourSaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourSaleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourSaleDto> Create([FromBody] TourSaleDto tourSale)
        {
            if (tourSale.TourIds.Count == 0)
            {
                return BadRequest("Please select tour.");
            }
            if (tourSale.EndDate < tourSale.StartDate)
            {
                return BadRequest("Bad request.");
            }
            if (!(tourSale.EndDate - tourSale.StartDate > TimeSpan.FromDays(14)))
            {
                var result = _tourSaleService.Create(tourSale);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("The sale is valid two weeks.");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourSaleDto> Get(int id)
        {
            var result = _tourSaleService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<TourSaleDto> Update([FromBody] TourSaleDto tourSale)
        {
            if (User.PersonId() != tourSale.AuthorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

            if (tourSale.TourIds.Count == 0)
            {
                return BadRequest("Please select tour.");
            }
            if (tourSale.EndDate < tourSale.StartDate)
            {
                return BadRequest("The sale end date cannot be before start date.");
            }
            if (!(tourSale.EndDate - tourSale.StartDate > TimeSpan.FromDays(14)))
            {
                var result = _tourSaleService.Update(tourSale);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("The sale is valid two weeks.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourSaleService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("toursOnSale/{saleId:int}")]
        public ActionResult<List<TourDto>> GetToursFromSale(int saleId)
        {
            var sale = _tourSaleService.Get(saleId);
            if (sale?.Value != null)
            {
                var tours = _tourService.GetToursFromSaleById(sale.Value.TourIds);
                return CreateResponse(tours);
            }
            else
            {
                return NotFound("Sale not found");
            }
        }
    }
}
