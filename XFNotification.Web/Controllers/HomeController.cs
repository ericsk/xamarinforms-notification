using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XFNotification.Web.Models;

namespace XFNotification.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Send(string DeviceType, string Devices, string Message)
        {
            NotificationOutcome outcome = null;
            HttpStatusCode ret = HttpStatusCode.InternalServerError;
            IList<string> toTags = null;

            if (!string.IsNullOrEmpty(Devices))
            {
                string[] tags = Devices.Split(new char[] { ',' });
                if (tags.Length > 0)
                {
                    toTags = new List<string>();
                    foreach (string tag in tags)
                    {
                        toTags.Add(tag.Trim());
                    }
                }
            }

            switch (DeviceType)
            {
                case "android":
                    var notif = "{\"data\":{\"message\":\"" + Message + "\"}}";
                    if (toTags == null)
                    {
                        outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(notif);
                    }
                    else
                    {
                        outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(notif, toTags);
                    }
                    break;
                case "ios":
                    var alert = "{\"apns\":{\"alert\":\"" + Message + "\"}}";
                    if (toTags == null)
                    {
                        outcome = await Notifications.Instance.Hub.SendAppleNativeNotificationAsync(alert);
                    }
                    else
                    {
                        outcome = await Notifications.Instance.Hub.SendAppleNativeNotificationAsync(alert, toTags);
                    }
                    break;
            }

            if (outcome != null)
            {
                if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
                    (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
                {
                    ret = HttpStatusCode.OK;
                }
            }

            return RedirectToAction("Index");
        }
    }
}