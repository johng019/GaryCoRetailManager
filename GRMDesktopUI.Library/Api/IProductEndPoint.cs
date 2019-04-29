using System.Collections.Generic;
using System.Threading.Tasks;
using GRMDesktopUI.Library.Models;

namespace GRMDesktopUI.Library.Api
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}