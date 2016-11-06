# Demo 2.2: Sending messages from Linux using Node.js

## Objectives

Show how a Linux device can connection to the same backend solution and send messages.

## Requirements

An active Azure subscription. If you don’t have an account, you can create a free trial account in just a couple of minutes. For details, see <http://azure.microsoft.com/pricing/free-trial/>

## Setup

You need the Remote Monitoring solution still running that you set up for Demo 2.1.

If you’re running Windows, download PuTTY from <http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html> and unzip the file. We will use PuTTY.exe to connect to the Linux virtual machine shortly.

Find the Solutions folder for this presentation, then the Demo 2.2 folder, and the file mydevice.js. You’ll need this file.

### Create the Linux VM

If you’re doing your presentation from a Linux PC then you can skip to Install NPM. If you are running Windows then you’ll create a Linux VM in Azure as follows.

1.  Head to the Azure portal, click on the new button in the left-side menu and type Ubuntu. Press Enter.

    <img src="./media/image1.png" width="407" height="356" />

2.  Choose one of the Ubuntu servers and click on the Create button.

    <img src="./media/image2.png" width="495" height="382" />

3.  Enter a unique virtual machine name, select HDD as the disk type, enter an admin user name and password, select your subscription and resource group. Click on the Create button.

    <img src="./media/image3.png" width="291" height="589" />

4.  When prompted for a pricing tier, select A1 Basic.

    <img src="./media/image4.png" width="304" height="469" />

5.  Click OK to create the VM. This will take a few minutes.

### Connect to the VM using PuTTY

1.  Locate the folder where you unzipped the PuTTY zip file and double-click on putty.exe

2.  Locate the virtual machine IP address in the Azure portal and type it in PuTTY.

    <img src="./media/image5.png" width="624" height="353" />

3.  Click Open.

4.  If you get the prompt from PuTTY, click Yes.

    <img src="./media/image6.png" width="432" height="294" />

5.  You’ll be prompted for your user name and password.

6.  You’re now connected to your Ubuntu virtual machine.

    <img src="./media/image7.png" width="624" height="395" />

### Install NPM

You now need to install NPM and the Azure Node module. Type the following:

1.  sudo apt-get update

2.  sudo apt-get install npm

3.  sudo npm install -g npm@latest

4.  sudo ln -s /usr/bin/nodejs /usr/bin/node

Install the Azure Node packages:

1.  sudo npm install -g azure-iot-device@latest

2.  sudo npm install -g azure-iot-device-amqp@latest

## Demo Steps

In this demo, you’ll explain how to send messages using Node on a Linux virtual machine.

Let’s new create a new device.

1.  Head to the Remote Monitoring Web site that you setup in Demo 2.1. Click on the Devices button in the left-side menu and Add a Device at the bottom of the menu.

    <img src="./media/image8.png" width="262" height="710" />

2.  Create a Custom device.

    <img src="./media/image9.png" width="624" height="224" />

3.  Select Let me define my own Device ID and type a unique name. Click the Check ID button to validate that the name is unique then click Create.

    <img src="./media/image10.png" width="410" height="275" />

4.  Select the newly created device and click on the Enable Device link in the right-side menu.

    <img src="./media/image11.png" width="624" height="192" />

5.  Head to the Azure portal, select the resource group for the Remote Monitoring solution and select the IoT Hub from the services list.

6.  Click on the Devices button and select the new device.

7.  Copy the device connection string.

    <img src="./media/image12.png" width="624" height="325" />

### Setup demo code

You now need to copy the code.

1.  On your Windows machine, open mydevice.js with Notepad the copy all the code

2.  Locate the connectionString and paste the value you copied earlier.

3.  Copy the whole code to the clipboard.

4.  In the Ubuntu console, launch the VIM editor by typing: vim mydevice.js

5.  Inside the console, right-click with your mouse, this will paste the code you copying earlier.

6.  Press the Escape key.

7.  Type :wq! (yes, that’s a *colon* followed by *w* *q* and an *exclamation mark*)

8.  You’re back at the command prompt.

9.  At the command prompt type: node mydevice.js

<!-- -->

1.  The device is now sending telemetry to IoT Hub.

    <img src="./media/image13.png" width="624" height="395" />

2.  Head to the Remote Monitoring Web site, select the new device from the dropdown menu. The graph will display the telemetry.

    <img src="./media/image14.png" width="624" height="312" />

3.  Press Control-C in the Ubuntu command prompt window to stop the node script.

4.  This completes this demo.

## Teardown

You should go to the Azure portal and delete the Linux VM you used. You will still need the Remote Monitoring solution (and the parts listed in the Azure portal) for other demos so don’t delete that stuff yet.
