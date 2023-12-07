using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/administrator/encounter")]
    [ApiController]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly IWebHostEnvironment _environment;
        public EncounterController(IEncounterService encounterService, IWebHostEnvironment environment)
        {
            _encounterService = encounterService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EncounterDto> Get(int id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EncounterDto> Create([FromBody] EncounterDto challenge)
        {
            var result = _encounterService.Create(challenge);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<EncounterDto> Update([FromBody] EncounterDto challenge)
        {
            var result = _encounterService.Update(challenge);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id);
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
    }
}
