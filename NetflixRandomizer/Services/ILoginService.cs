
namespace NetflixRandomizer.Services
{
    public interface ILoginService
    {
        Task<bool> CheckUser(string user, string pass);
    }

    public class LoginService : ILoginService
    {
        public async Task<bool> CheckUser(string user, string pass)
        {
            //TODO: llamar a api
            return true;
        }
    }

    public class LoginServiceMockup : ILoginService
    {
        public async Task<bool> CheckUser(string user, string pass)
        {
            //Simulamos que tarda en cargar el usuario y pass
            await Task.Delay(2000);
            return user.Equals("Admin") && pass.Equals("Admin");
        }
    }

}
