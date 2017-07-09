
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using In_App_Notification_POC_Simple.Models;
using Enum = In_App_Notification_POC_Simple.Models.Enum;

namespace In_App_Notification_POC_Simple.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:62027") };
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDetailDm loginDetail)
        {

            var response =
                await _client.GetAsync($"/login?userName={loginDetail.UserName}&password={loginDetail.Password}");
            if (response.IsSuccessStatusCode)
            {
                loginDetail = response.Content.ReadAsAsync<LoginDetailDm>().Result;
            }
            switch (loginDetail.RoleId)
            {
                case (int)Enum.Role.Employee:
                    Session["SessionData"] = loginDetail;
                    return RedirectToAction("EmployeeNotification", "Notification");

                case (int)Enum.Role.Manager:
                    Session["SessionData"] = loginDetail;
                    return RedirectToAction("AddNotification", "Notification");
                case (int)Enum.Role.Admin:

                    Session["SessionData"] = loginDetail;
                    return RedirectToAction("AddNotification", "Notification");
                default:
                    return RedirectToAction("");
            }

        }

    }
}