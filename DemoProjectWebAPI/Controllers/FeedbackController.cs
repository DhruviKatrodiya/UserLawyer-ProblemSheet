using DemoProjectWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public readonly LegalProDBContext _legalProDBContext;
        public FeedbackController(LegalProDBContext legalProDBContext)
        {
            _legalProDBContext = legalProDBContext;
        }

        [HttpPost("PostFeedback")]
        public async Task<ActionResult<IEnumerable<TblFeedback>>> PostFeedback([FromForm] string feedback, int rate, int userId, int lawyerId)
        {
            var feedbacks = new TblFeedback();
            feedbacks.Feedback = feedback;
            feedbacks.Rate = rate;  
            feedbacks.UserId = userId;
            feedbacks.LawyerId = lawyerId;
            _legalProDBContext.Feedbacks.Add(feedbacks);
            await _legalProDBContext.SaveChangesAsync();
            return Ok(feedbacks);
        }

        [HttpGet("DisplayAllFeedbacks")]
        public async Task<ActionResult<IEnumerable<TblFeedback>>> DisplayAllFeedbacks()
        {
            var result = await _legalProDBContext.Feedbacks.Select(f => new { f.Feedback, f.Rate, f.UserId,f.TblUser.FirstName, f.LawyerId, f.TblLawyer.LawyerName }).ToListAsync();
            return Ok(result);
        }
    }
}
