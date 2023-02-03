using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProjectWebAPI.Models
{
    public class TblLawyer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lawyer name is required")]
        [MaxLength(50)]
        public string LawyerName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(10)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(10)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        
        //[ForeignKey(nameof(TblUser))]
        //public int UserId { get; set; }
        //public TblUser? User { get; set; }
    }
}
