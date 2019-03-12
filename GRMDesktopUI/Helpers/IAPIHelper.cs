using System.Threading.Tasks;
using GRMDesktopUI.Models;

namespace GRMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}