using System.Web.Services;

namespace In_App_Notification_POC_Simple.Comet
{
    /// <summary>
    /// Summary description for Dispatcher
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Dispatcher : WebService
    {

        [WebMethod]
        public bool EmployeeWaitMessage(string userName)
        {
            return ClientAdapter.Instance.EmployeeGetMessage(userName);
        }
        public bool ManagerWaitMessage(string userName)
        {
            return ClientAdapter.Instance.ManagerGetMessage(userName);
        }
    }
}
