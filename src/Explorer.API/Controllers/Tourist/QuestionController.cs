using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [HttpPost]
        public ActionResult<QuestionDto> Create([FromBody] QuestionDto questionDto)
        {

            var result = _questionService.Create(questionDto);


            return CreateResponse(result);
        }
    }
}
