namespace DemoProjectWebAPI.Services
{
    public interface IUserService
    {
        bool ValidateCredentials(string Username,string Password);
    }
}
