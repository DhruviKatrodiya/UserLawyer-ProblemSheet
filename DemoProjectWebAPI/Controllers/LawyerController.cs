using DemoProjectWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoProjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : ControllerBase
    {
        public readonly LegalProDBContext _legalProDBContext;
        public LawyerController(LegalProDBContext legalProDBContext)
        {
            _legalProDBContext = legalProDBContext;
        }

        [HttpGet("DisplayAllLawyer")]
        public async Task<ActionResult<IEnumerable<TblLawyer>>> DisplayAllLawyer()
        {
            return await _legalProDBContext.Lawyers.ToListAsync();
        }

        [HttpPost("CreateLawyer")]
        public async Task<ActionResult<IEnumerable<TblLawyer>>> CreateLawyer([FromForm] TblLawyer lawyer)
        {
            //Validate
            if (_legalProDBContext.Lawyers.Any(u => u.UserName == lawyer.UserName))
            {
                throw new Exception("In the Username of the lawyer '" + lawyer.UserName + "' is already taken");
            }

            //hash password
            lawyer.Password = BCrypt.Net.BCrypt.HashPassword(lawyer.Password);

            _legalProDBContext.Lawyers.Add(lawyer);
            await _legalProDBContext.SaveChangesAsync();
            return Ok(lawyer);
        }

        [HttpGet("DisplayAverageRating")]
        public async Task<ActionResult<IEnumerable<TblLawyer>>> DisplayAverageRating(int Id)
        {
            var rate = from r in _legalProDBContext.Feedbacks
                       join l in _legalProDBContext.Lawyers on r.LawyerId equals l.Id
                       where l.Id == Id 
                       select r.Rate;
            var avg = rate.Average();
            return Ok(avg);
        }

    }
}
