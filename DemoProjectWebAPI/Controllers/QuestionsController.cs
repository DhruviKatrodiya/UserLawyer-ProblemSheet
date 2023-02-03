using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DemoProjectWebAPI.Models;
using dotenv.net;
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
    public class QuestionsController : ControllerBase
    {
        public readonly LegalProDBContext _legalProDBContext;
        public QuestionsController(LegalProDBContext legalProDBContext)
        {
            _legalProDBContext = legalProDBContext;
        }

        [HttpGet("DisplayAllQuestions")]
        public async Task<ActionResult<IEnumerable<TblQuestions>>> DisplayAllQuestions()
        {
            var result = await _legalProDBContext.Questions.Select(q => new { q.Questions,q.User,q.Lawyer}).ToListAsync();
            return Ok(result);
        }

        [HttpPost("CreateQuestions")]
        public async Task<ActionResult<IEnumerable<TblQuestions>>> CreateQuestions([FromForm] string questions,bool isquestion,string description,IFormFile file,int userId,int? lawyerId)
        {
            var question = new TblQuestions();
            question.Questions = questions;
            question.Description = description;
            question.IsQuestion = isquestion;
            question.UserId = userId;
            question.LawyerId = lawyerId;
            var File = question.file = file;
            string path = await UploadMedia(File);
            question.MediaFile = path;

            if (_legalProDBContext.Questions.Any(q => q.Questions == question.Questions))
            {
                throw new Exception("Questions '" + question.Questions + "' is already given");
            }

            _legalProDBContext.Questions.Add(question);
            await _legalProDBContext.SaveChangesAsync();         
            return Ok(question);
        }

        private async Task<string> UploadMedia(IFormFile file)
        {

            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            cloudinary.Api.Secure = true;
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = true
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            Console.WriteLine(uploadResult.JsonObj);
            var secureUrl = uploadResult.SecureUrl;
            return secureUrl.ToString();

        }

        [HttpGet("NotPickedQuestions")]
        public async Task<ActionResult<IEnumerable<TblQuestions>>> NotPickedQuestions()
        {
            //var result = from q in _legalProDBContext.Questions
            //             where q.LawyerId == null || q.IsQuestion == false
            //             select q;
            var r = await _legalProDBContext.Questions.Where(q => q.LawyerId == null || q.IsQuestion == false).ToListAsync();
            return Ok(r);
        }

        [HttpPut("PickedQuestions")]
        public async Task<ActionResult<IEnumerable<TblQuestions>>> PickedQuestions(int lawyerId,int questionId)
        {
            var tblQuestions = await _legalProDBContext.Questions.FindAsync(questionId);
            tblQuestions.LawyerId = lawyerId;
            tblQuestions.IsQuestion = true;
            _legalProDBContext.Entry(tblQuestions).State = EntityState.Modified;
            await _legalProDBContext.SaveChangesAsync();
            return Ok(tblQuestions);
        }


    }
}
