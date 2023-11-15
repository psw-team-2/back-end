using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Public.Author;

namespace Explorer.API.Controllers.Author
{
    [Route("api/administration/object")]
    public class ObjectController : BaseApiController
    {
        private readonly IObjectService _objectService;
        private readonly IWebHostEnvironment _environment;

        public ObjectController(IObjectService objectService, IWebHostEnvironment environment)
        {
            _objectService = objectService;
            _environment = environment;
        }

        [HttpPost]
        public ActionResult<ObjectDto> Create([FromBody] ObjectDto objectDto)
        {
            var result = _objectService.Create(objectDto);
            return CreateResponse(result);
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "Images", file.FileName);
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
        public ActionResult<ObjectDto> Get(int id)
        {
            var result = _objectService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ObjectDto> Update([FromBody] ObjectDto object1)
        {
            var result = _objectService.Update(object1);
            return CreateResponse(result);
        }
    }
}
