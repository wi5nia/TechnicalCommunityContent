// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.Devices.Client.Samples
{
    class Program
    {

        // String containing Hostname, Device Id & Device Key in one of the following formats:
        //  "HostName=<iothub_host_name>;DeviceId=<device_id>;SharedAccessKey=<shared_access_key>"
        //  "HostName=<iothub_host_name>;CredentialType=SharedAccessSignature;DeviceId=<device_id>;SharedAccessSignature=SharedAccessSignature sr=<iot_host>/devices/<device_id>&sig=<token>&se=<expiry_time>";
        private const string DeviceConnectionString = "PASTE THE CONNECTION STRING FOR THE DEVICE";
        private const string DeviceId = "PASTE THE DEVICE ID";

        // Number of messages to send
        private static int MESSAGE_COUNT = 5;

        static void Main(string[] args)
        {
            try
            {
                DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

                if (deviceClient == null)
                {
                    Console.WriteLine("Failed to create DeviceClient!");
                }
                else
                {
                    SendEvent(deviceClient).Wait();
                    ReceiveCommands(deviceClient).Wait();
                }

                Console.WriteLine("Exited!\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sample: {0}", ex.Message);
            }
        }

        static async Task SendEvent(DeviceClient deviceClient)
        {
            string dataBuffer;
            Random rnd = new Random();

            Console.WriteLine("Device will send {0} messages to IoTHub...\n", MESSAGE_COUNT);

            for (int count = 0; count < MESSAGE_COUNT; count++)
            {
                var message = new { DeviceId = DeviceId, Temperature = rnd.Next(40, 50), Humidity = rnd.Next(50, 60), ExternalTemperature = rnd.Next(70, 80) };

                // Uncomment the following line for demo 2.5
                //message = new { DeviceId = DeviceId, Temperature = 105, Humidity = 80, ExternalTemperature = 80 };

                dataBuffer = JsonConvert.SerializeObject(message);
                Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
                Console.WriteLine("\t{0}> Sending message: {1}, Data: [{2}]", DateTime.Now.ToLocalTime(), count, dataBuffer);

                await deviceClient.SendEventAsync(eventMessage);

                Thread.Sleep(2000);
            }
        }

        static async Task ReceiveCommands(DeviceClient deviceClient)
        {
            Console.WriteLine("\nDevice is now waiting for commands from IoTHub...\n");
            Message receivedMessage;
            string messageData;

            while (true)
            {
                receivedMessage = await deviceClient.ReceiveAsync(TimeSpan.FromSeconds(1));

                if (receivedMessage != null)
                {
                    messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    Console.WriteLine("\t{0}> Received message: {1}", DateTime.Now.ToLocalTime(), messageData);

                    dynamic message = JsonConvert.DeserializeObject(messageData);
                    Console.WriteLine("\t\tCommand received: {0}", message.Name);
                }
            }
        }
    }
}
