using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Route("api/addcheckpoint/checkpoint")]
    public class CheckPointController : BaseApiController
    {
        private readonly ICheckPointService _checkPointService;
        private readonly IWebHostEnvironment _environment;

        public CheckPointController(ICheckPointService checkPointService, IWebHostEnvironment environment)
        {
            _checkPointService = checkPointService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult<PagedResult<CheckPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _checkPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CheckPointDto> Create([FromBody] CheckPointDto checkPoint)
        {
            var result = _checkPointService.Create(checkPoint);
            return CreateResponse(result);
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "Images", fName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return $"{file.FileName} successfully uploaded to the Server";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<CheckPointDto> Get(int id)
        {
            var result = _checkPointService.Get(id);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<CheckPointDto> Update([FromBody] CheckPointDto checkPoint)
        {
            var result = _checkPointService.Update(checkPoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _checkPointService.Delete(id);
            return CreateResponse(result);
        }
    }
}
