using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using In_AppNotification_POC_WebAPI.Models;
using In_App_Notification_POC_Simple.Models;

namespace In_AppNotification_POC_WebAPI.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly InAppNotificationEntities _inAppNotificationEntities = new InAppNotificationEntities();

        [HttpGet, Route("")]
        public LoginDetail GetLoginUserDetails(string userName, string password)
        {
            var result = _inAppNotificationEntities.LoginDetails.FirstOrDefault(
                x =>
                    x.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase) &&
                    x.Password.Equals(password));
            return result;
        }

        [HttpGet, Route("users")]
        public List<LoginDetailDm> GetUsers()
        {
            var result = _inAppNotificationEntities.LoginDetails.ToList().Select(x => new LoginDetailDm()
            {
                UserName = x.UserName,
                Id = x.Id,
                RoleId = x.RoleId
            }).ToList();
            return result;
        }
    }
}
