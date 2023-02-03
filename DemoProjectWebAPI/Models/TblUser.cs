using System.ComponentModel.DataAnnotations;

namespace DemoProjectWebAPI.Models
{
    public class TblUser
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Roles { get; set; }

    }
}
