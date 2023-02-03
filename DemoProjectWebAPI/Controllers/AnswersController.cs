using DemoProjectWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        public readonly LegalProDBContext _legalProDBContext;
        public AnswersController(LegalProDBContext legalProDBContext)
        {
            _legalProDBContext = legalProDBContext;
        }

        [HttpPost("PostAnswer")]
        public async Task<ActionResult<IEnumerable<TblAnswer>>> CreateAnswer([FromForm] string answer,bool isanswer,int userId,int questionId)
        {
            var answers = new TblAnswer();
            answers.Answers = answer;
            answers.IsAnswer = isanswer;
            answers.UserId = userId;
            answers.QuestionId = questionId;
            _legalProDBContext.Answers.Add(answers);
            await _legalProDBContext.SaveChangesAsync();
            return Ok(answers);
        }

        [HttpGet("DisplayAllAnswers")]
        public async Task<ActionResult<IEnumerable<TblAnswer>>> DisplayAllAnswers()
        {
            //var result = await _legalProDBContext.Answers.Select(a => new { a.Answers, a.IsAnswer, a.UserId,a.Questions }).ToListAsync();
            var result = from a in _legalProDBContext.Answers
                         where a.IsAnswer == true
                         select new
                         {
                             a.Answers,
                             a.IsAnswer,
                             a.UserId,
                             a.Questions
                         };
            return Ok(result);
        }
    }
}
