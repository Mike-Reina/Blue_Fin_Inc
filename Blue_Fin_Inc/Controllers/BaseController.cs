using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class BaseController : Controller
    {
        public void Notify(string message, string title = "Blue Fin Inc", string provider = "sweetAlert",
                                    NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = provider
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        //private string GetProvider()
        //{
        //    var builder = new ConfigurationBuilder()
        //                    .SetBasePath(Directory.GetCurrentDirectory())
        //                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //                    .AddEnvironmentVariables();

        //    IConfigurationRoot configuration = builder.Build();

        //    var value = configuration["NotificationProvider"];

        //    return value;
        //}
    }
}
