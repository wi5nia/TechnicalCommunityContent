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

        private static RegistryManager registryManager;
        private static string iotHubConnectionString = ConfigurationManager.AppSettings["iotHubConnectionString"]; 
        private string iotHubUri = ConfigurationManager.AppSettings["iotHubUri"];


        static HomeController()
        {
            registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }


        #region Lab 1

        public async Task<ActionResult> Lab1()
        {
            await AddDeviceAsync();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SendMessage(string id, string key, int value)
        {            
            var telemetryDataPoint = new
            {
                deviceId = id,
                telemetry = value
            };

            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

            var deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(id, key));

            await deviceClient.SendEventAsync(message);

            Response.StatusCode = 200; // OK = 200
            return null;    
        }

        #endregion


        #region Admin

        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAllDevices()
        {
            await DeleteAllDevicesAsync();

            Response.StatusCode = 200; // OK = 200
            return null;
        }

        private async Task DeleteAllDevicesAsync()
        {
            var devices = await registryManager.GetDevicesAsync(1000);

            if (devices.Count<Device>() > 0)
                await registryManager.RemoveDevices2Async(devices);

        }

        private async Task AddDeviceAsync()
        {

            var deviceGuid = Guid.NewGuid();          

            var devices = await registryManager.GetDevicesAsync(1000);
            string deviceId = "Device" + (devices.Count<Device>() + 1) + "-" + deviceGuid.ToString();

            var device = await registryManager.GetDeviceAsync(deviceId);

            if (device == null)
            {
                device = new Device(deviceId);
                device = await registryManager.AddDeviceAsync(device);
            }

            ViewData["id"] = device.Id;
            ViewData["key"] = device.Authentication.SymmetricKey.PrimaryKey;

        }



        #endregion

    }
}