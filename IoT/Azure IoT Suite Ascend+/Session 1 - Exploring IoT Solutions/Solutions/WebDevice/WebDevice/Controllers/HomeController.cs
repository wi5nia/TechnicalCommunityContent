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

        private const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";
        private static string AccountKey = ConfigurationManager.AppSettings["textAnalyticsKey"];
        private const int NumLanguages = 1;

        static HomeController()
        {
            registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }


        #region Demo 1

        public async Task<ActionResult> Demo1()
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

        #region Demo 2

        public async Task<ActionResult> Demo2()
        {
            await AddDeviceAsync();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SendComment(string id, string key, string value)
        {

            var _language = await DetectLanguage(value);
            var _sentiment = await DetectSentiment(value);

            dynamic _languageConverted = JsonConvert.DeserializeObject(_language);
            dynamic _sentimentConverted = JsonConvert.DeserializeObject(_sentiment);

            var commentDataPoint = new
            {
                deviceId = id,
                comment = value,
                language = _languageConverted.documents[0].detectedLanguages[0].name.ToString(),
                languageScore = float.Parse(_languageConverted.documents[0].detectedLanguages[0].score.ToString()),
                sentiment = float.Parse(_sentimentConverted.documents[0].score.ToString())
            };

            var commentString = JsonConvert.SerializeObject(commentDataPoint);
            var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(commentString));

            var deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(id, key));

            await deviceClient.SendEventAsync(message);
        
            Response.StatusCode = 200; // OK = 200
            return Json(commentDataPoint, JsonRequestBehavior.AllowGet); ; // Json(commentDataPoint);
            
        }

        static async Task<string> DetectLanguage(string comment)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Request body. Insert your text data here in JSON format.
                byte[] byteData = Encoding.UTF8.GetBytes("{\"documents\":[" +
                    "{\"id\":\"1\",\"text\":\"" + comment + "\"}" +
                    "]}");

                // Detect language:
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["numberOfLanguagesToDetect"] = NumLanguages.ToString(CultureInfo.InvariantCulture);
                var uri = "text/analytics/v2.0/languages?" + queryString;
                var response = await CallEndpoint(client, uri, byteData);

                return response;              
            }
        }

        static async Task<string> DetectSentiment(string comment)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Request body. Insert your text data here in JSON format.
                byte[] byteData = Encoding.UTF8.GetBytes("{\"documents\":[" +
                    "{\"id\":\"1\",\"text\":\"" + comment + "\"}" +
                    "]}");

                // Detect sentiment:
                var uri = "text/analytics/v2.0/sentiment";
                var response = await CallEndpoint(client, uri, byteData);

                return response;
            }
        }

        static async Task<String> CallEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
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

        private async Task DeleteAllDevicesAsync()
        {
            var devices = await registryManager.GetDevicesAsync(1000);

            if (devices.Count<Device>() > 0)
                await registryManager.RemoveDevices2Async(devices);

        }

        #endregion

    }
}