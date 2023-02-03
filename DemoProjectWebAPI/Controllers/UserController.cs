using AutoMapper;
using BCrypt.Net;
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
    public class UserController : ControllerBase
    {
        public readonly LegalProDBContext _legalProDBContext;
        public UserController(LegalProDBContext legalProDBContext)
        {
            _legalProDBContext = legalProDBContext;
        }

        [HttpGet]
        [Route("displayAllUsers")]
        public async Task<ActionResult<IEnumerable<TblUser>>> DisplayAllUsers()
        {
            return await _legalProDBContext.Users.ToListAsync();
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<IEnumerable<TblUser>>> CreateUser([FromForm] TblUser user)
        {
            //Validate
            if(_legalProDBContext.Users.Any(u => u.UserName == user.UserName))
            {
                throw new Exception("Username '" + user.UserName + "' is already taken");
            }

            //hash password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _legalProDBContext.Users.Add(user);
            await _legalProDBContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("UserLawyerConversation")]
        public async Task<IActionResult> UserLawyerConversation(int userId)
        {
            var conversation = from q in _legalProDBContext.Questions
                               join a in _legalProDBContext.Answers
                               on q.Id equals a.QuestionId
                               where q.UserId == userId
                               select new
                               {
                                   q.Questions,
                                   a.Answers
                               };
            return Ok(conversation);
        } 
    }
}
