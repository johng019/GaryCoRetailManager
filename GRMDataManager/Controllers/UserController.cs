using System.Collections.Generic;
using System.Web.Http;
using System.Web;
using GRMDataManager.Library;
using GRMDataManager.Library.DataAccess;
using GRMDataManager.Library.Models;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace GRMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    { 
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserByID(userId).First();
        }
    }
}
