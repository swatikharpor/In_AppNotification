using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using In_App_Notification_POC_Simple.Comet;
using In_App_Notification_POC_Simple.Models;


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
            var response = new NotificationDm();
            notification.Id = Guid.NewGuid().ToString();
            notification.NotificationFrom = user.Id;
            var result = await _client.PostAsJsonAsync("/notifications", notification);
            if (result.IsSuccessStatusCode)
            {
                response = result.Content.ReadAsAsync<NotificationDm>().Result;
                var message = new Message
                {
                    RecipientName = "swati",
                    MessageContent = "hello"
                };
                // var clientAdapter = new ClientAdapter();
                var state = ClientAdapter.Instance.EmployeeSendMessage(message);
            }

            return View("ManagerNotification");
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
            // var user = (LoginDetailDm)Session["SessionData"];
            //var result = _client.GetAsync($"/notifications/{user.Id}/unread");
            //var response = new List<NotificationDm>();
            //if (result.Result.IsSuccessStatusCode)
            //{
            //    response = result.Result.Content.ReadAsAsync<List<NotificationDm>>().Result;

            //}
            return View("EmployeeNotification");
        }

        public JsonResult GetUnreadNotifications_Count()

        {
            var user = (LoginDetailDm)Session["SessionData"];
            var count = _client.GetAsync($"notifications/{user.Id}/unread/count");
            var c = count.Result.Content.ReadAsAsync<int>().Result;
            return count.Result.IsSuccessStatusCode
                ? Json(c, JsonRequestBehavior.AllowGet)
                : Json("error", JsonRequestBehavior.DenyGet);

        }

        public ActionResult ManagerNotification()
        {
            return View("ManagerNotification");
        }

        public JsonResult ManagerWaitMessage(string userName)
        {
            var state = new bool();


            var dispatcher = new Dispatcher();
            state = dispatcher.ManagerWaitMessage(userName);
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployeeWaitMessage(string userName)
        {
            var state = new bool();


            var dispatcher = new Dispatcher();
            state = dispatcher.EmployeeWaitMessage(userName);

            return Json(state, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNotificationBarPartial()
        {
            // var result = _client.GetAsync($"/notifications/{notificationId}");
            // var response = new NotificationDm();
            return PartialView("_NotificationBar");
        }
    }
}
