namespace DemoProjectWebAPI.Services
{
    public class UserServices : IUserService
    {
        public bool ValidateCredentials(string Username, string Password)
        {
            return Username.Equals("Admin") && Password.Equals("Password");
        }
    }
}
