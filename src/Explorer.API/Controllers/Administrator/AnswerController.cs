using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/answer")]
    [ApiController]
    public class AnswerController : BaseApiController
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        public ActionResult<AnswerDto> Create([FromBody] AnswerDto answer)
        {
            var result = _answerService.Create(answer);
            return CreateResponse(result);
        }
    }
}
