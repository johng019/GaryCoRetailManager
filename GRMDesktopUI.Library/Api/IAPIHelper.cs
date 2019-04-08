using GRMDesktopUI.Models;
using System.Threading.Tasks;


namespace GRMDesktopUI.Library.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}