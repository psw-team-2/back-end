using Explorer.Payments.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
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

        [HttpPost("createAnswer")]
        public ActionResult<AnswerDto> Create([FromBody] AnswerDto answer)
        {
            var result = _answerService.Create(answer);
            return CreateResponse(result);
        }

        [HttpGet("byQuestionId")]
        public ActionResult<AnswerDto> GetAnswerByQuestionId(int questionId)
        {
            var result = _answerService.GetAnswerByQuestionId(questionId);

            if (result == null)
            {
                return new ObjectResult(new { Message = "Not found answer for that question id.", StatusCode = 400 }) { StatusCode = 400 };
            }

            return CreateResponse(result);
        }
    }
}
