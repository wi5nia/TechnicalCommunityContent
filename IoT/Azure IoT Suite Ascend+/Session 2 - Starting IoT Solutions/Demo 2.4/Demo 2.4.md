# DEMO 2.4: Sending messages from a Raspberry Pi running Raspian

## Objectives

Show how a physical device can connect to the same backend and send messages. If you don’t have a device you can skip this demo.

## Requirements

An active Azure subscription. If you don’t have an account, you can create a free trial account in just a couple of minutes. For details, see <http://azure.microsoft.com/pricing/free-trial/>

A Raspberry Pi 2 or 3 with the latest version of Raspbian installed.

## Setup

You need the Remote Monitoring solution still running that you set up for Demo 2.1.

Find the Solutions folder for this presentation, then the Demo 2.4 folder, and the file mydevice.js. You’ll need this file.

If not already installed, install NPM on the Raspberry Pi. Open a command prompt and type the following:

1.  sudo apt-get install npm

2.  sudo npm install -g npm@latest

Install the Azure Node packages:

1.  sudo npm install -g azure-iot-device@latest

2.  sudo npm install -g azure-iot-device-amqp@latest

Copy the file mydevice.js to the Raspberry Pi and save the file as mydevice.js.

## Demo Steps

In this demo, you’ll explain how to send messages using Node on a Raspberry Pi.

Let’s first create a new device.

1.  Head to the Remote Monitoring Web site. Click on the Devices button in the left-side menu and Add a Device at the bottom of the menu.

    <img src="./media/image1.png" width="262" height="710" />

2.  Create a Custom device.

    <img src="./media/image2.png" width="624" height="224" />

3.  Select Let me define my own Device ID and type a unique name. Click the Check ID button to validate that the name is unique then click Create.

    <img src="./media/image3.png" width="624" height="406" />

4.  Select the newly created device and click on the Enable Device link in the right-side menu.

    <img src="./media/image4.png" width="624" height="214" />

5.  Head to the Azure portal, select the resource group for the Remote Monitoring solution and select the IoT Hub from the services list.

6.  Click on the Devices button and select the new device.

7.  Copy the device connection string and paste as the connectionString variable value.

    <img src="./media/image5.png" width="569" height="335" />

8.  Save the file.

9.  At the command prompt type: node mydevice.js

10. The device is now sending telemetry to IoT Hub.

11. Head to the Remote Monitoring Web site, select the new device from the dropdown menu. The graph will display the telemetry.

    <img src="./media/image6.png" width="624" height="217" />

12. Press Control-C in the Raspberry Pi command prompt window to stop the node script.

13. This completes this demo.

## Teardown

You will still need the Remote Monitoring solution (and the parts listed in the Azure portal) for other demos so don’t delete that stuff yet.
