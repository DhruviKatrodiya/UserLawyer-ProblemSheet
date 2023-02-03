using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProjectWebAPI.Models
{
    public class TblQuestions
    {
        [Key]
        public int Id { get; set; }
        public string? Questions { get; set; }
        public bool? IsQuestion { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        public string? MediaFile { get; set; }

        [ForeignKey(nameof(TblUser))]
        public int UserId { get; set; }
        public TblUser? User { get; set; }

        [ForeignKey(nameof(TblLawyer))]
        public int? LawyerId { get; set; }      
        public TblLawyer? Lawyer { get; set; }

    }
}
