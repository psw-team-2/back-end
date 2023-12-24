using Explorer.BuildingBlocks.Core.UseCases;
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

        [HttpGet]
        public ActionResult<PagedResult<QuestionDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _questionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<QuestionDto> Create([FromBody] QuestionDto questionDto)
        {

            var result = _questionService.Create(questionDto);


            return CreateResponse(result);
        }
        [HttpGet("unanswered")]
        public ActionResult<PagedResult<QuestionDto>> GetAllUnanswered()
        {
            var result = _questionService.GetAllUnanswered();
            return CreateResponse(result);
        }

        [HttpGet("answered")]
        public ActionResult<PagedResult<QuestionDto>> GetAllAanswered()
        {
            var result = _questionService.GetAllAnswered();
            return CreateResponse(result);
        }
    }
}
