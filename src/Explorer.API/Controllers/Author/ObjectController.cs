using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Public.Author;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/object")]

    public class ObjectController : BaseApiController
    {

        private readonly IObjectService _objectService;


        public ObjectController(IObjectService objectService)
        {
            _objectService = objectService;
        }




        [HttpPost]
        public ActionResult<ObjectDto> Create([FromBody] ObjectDto objectDto)
        {
            var result = _objectService.Create(objectDto);
            return CreateResponse(result);
        }

    }
}
