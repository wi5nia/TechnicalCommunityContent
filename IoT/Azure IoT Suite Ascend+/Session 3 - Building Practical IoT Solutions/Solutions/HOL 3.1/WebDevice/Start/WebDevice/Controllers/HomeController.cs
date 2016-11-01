using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Globalization;

namespace WebDevice.Controllers
{
    public class HomeController : Controller
    {

        static HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }


        #region Lab 1

        public async Task<ActionResult> Lab1()
        {
            return View();
        }


        #endregion


        #region Admin

        public ActionResult Admin()
        {
            return View();
        }

  
        #endregion

    }
}