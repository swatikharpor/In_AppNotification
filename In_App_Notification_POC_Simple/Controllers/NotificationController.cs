using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using In_App_Notification_POC_Simple.Models;
using Microsoft.Ajax.Utilities;

namespace In_App_Notification_POC_Simple.Controllers
{
    public class NotificationController : Controller
    {
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:62027") };
        public async Task<ActionResult> AddNotification()
        {
            var response = new List<LoginDetailDm>();
            var result = await _client.GetAsync("/login/users");
            if (result.IsSuccessStatusCode)
            {
                response = result.Content.ReadAsAsync<List<LoginDetailDm>>().Result;
                ViewBag.To = new SelectList(response, "Id", "UserName");
            }
            return result.IsSuccessStatusCode ? View("AddNotification1") : View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> AddNotification(NotificationDm notification)
        {
            var user = (LoginDetailDm)Session["SessionData"];
            var response = new bool();
            notification.Id = Guid.NewGuid().ToString();
            notification.NotificationFrom = user.Id;
            var result = await _client.PostAsJsonAsync("/notifications", notification);
            if (result.IsSuccessStatusCode)
            {
                response = result.Content.ReadAsAsync<bool>().Result;
            }

            return RedirectToAction("AddNotification");
        }

        public async Task<JsonResult> GetTopTenNotifications()
        {
            ViewBag.Message = "Your application description page.";
            var user = (LoginDetailDm)Session["SessionData"];
            var result = await _client.GetAsync($"/notifications/{user.Id}/topten");
            var response = new List<NotificationDm>();
            if (result.IsSuccessStatusCode)
            {
                response = result.Content.ReadAsAsync<List<NotificationDm>>().Result;

            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetNotificationById(string notificationId)
        {
            var result = _client.GetAsync($"/notifications/{notificationId}");
            var response = new NotificationDm();
            if (result.Result.IsSuccessStatusCode)
            {
                response = result.Result.Content.ReadAsAsync<NotificationDm>().Result;
            }
            return PartialView("_GetNotificationById", response);
        }

        public ActionResult EmployeeNotification()
        {
            var user = (LoginDetailDm)Session["SessionData"];
            var result = _client.GetAsync($"/notifications/{user.Id}/unread");
            var response = new List<NotificationDm>();
            if (result.Result.IsSuccessStatusCode)
            {
                response = result.Result.Content.ReadAsAsync<List<NotificationDm>>().Result;

            }
            return View("_NotificationBar", response);
        }

        public JsonResult GetUnreadNotificationsCount()
        {
            var user = (LoginDetailDm)Session["SessionData"];
            var count = _client.GetAsync($"notifications/{user.Id}/unread/count");
            // var c = count.Result.Content.ReadAsAsync<int>().Result;
            return count.Result.IsSuccessStatusCode
                ? Json(count.Result.Content.ReadAsAsync<int>().Result, JsonRequestBehavior.AllowGet)
                : Json("error", JsonRequestBehavior.DenyGet);

        }
    }
}