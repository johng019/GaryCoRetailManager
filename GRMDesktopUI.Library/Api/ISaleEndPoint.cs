using System.Threading.Tasks;
using GRMDesktopUI.Library.Models;

namespace GRMDesktopUI.Library.Api
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}