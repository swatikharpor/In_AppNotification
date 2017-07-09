using System.Web;
using System.Web.Mvc;

namespace In_App_Notification_POC_Simple
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
