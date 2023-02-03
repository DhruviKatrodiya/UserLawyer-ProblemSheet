using Microsoft.EntityFrameworkCore;

namespace DemoProjectWebAPI.Models
{
    public class LegalProDBContext : DbContext
    {
        public LegalProDBContext(DbContextOptions<LegalProDBContext> options) : base(options)
        {

        }
        public DbSet<TblUser> Users { get; set; }
        public DbSet<TblLawyer> Lawyers { get; set; }
        public DbSet<TblQuestions> Questions { get; set; }
        public DbSet<TblAnswer> Answers { get; set; }
        public DbSet<TblFeedback> Feedbacks { get; set; }
    }
}
