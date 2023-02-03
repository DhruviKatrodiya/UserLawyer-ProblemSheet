using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProjectWebAPI.Models
{
    public class TblFeedback
    {
        [Key]
        public int Id { get; set; }
        public string Feedback { get; set; }
        public int Rate { get; set; }

        [ForeignKey(nameof(TblUser))]
        public int UserId { get; set; }
        public TblUser? TblUser { get; set; }

        [ForeignKey(nameof(TblLawyer))]
        public int LawyerId { get; set; }

        public TblLawyer? TblLawyer { get; set; }
    }
}
