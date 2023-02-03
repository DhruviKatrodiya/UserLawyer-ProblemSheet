using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProjectWebAPI.Models
{
    public class TblAnswer
    {
        [Key]
        public int Id { get; set; }
        public string Answers { get; set; }
        public bool IsAnswer { get; set; }

        [ForeignKey(nameof(TblUser)), Column(Order = 0)]
        public int UserId { get; set; }
        public TblUser? User { get; set; }

        [ForeignKey(nameof(TblQuestions)), Column(Order = 1)]
        public int QuestionId { get; set; }
        public TblQuestions? Questions { get; set; }
       
        
        

    }
}
