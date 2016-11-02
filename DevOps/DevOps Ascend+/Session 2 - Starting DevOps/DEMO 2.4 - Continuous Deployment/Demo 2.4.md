# Demo 4 Parts Unlimited MRP - Continuous Deployment

### Duration
This demo should take ~ 6 minutes

### Goal
Introduction and high-level overview of the Continuous Deployment DevOps principal in Parts Unlimited MRP. 

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* An Internet connection
* Visual Studio Code or any text editor

# Demo

## Explain the build functionalities

0. Browse to **Build** tab

 ![](<media/browse_build_tab.png>)

 > Being able to build, test your code & produce packages is the responsibility of your CI build. 
 >
 > Let's take a look how is it done for the MRP app.

0. Browse to **Release** tab

 ![](<media/browse_release_tab.png>)

 > In your CD build you'll deploy to your environments using artifacts & packages your CI process has produced.

0. Right-click the **PartsUnlimitedMRPCD** CD build and click **Edit**

 ![](<media/edit_pumrpcd_build.png>)

0. Click the **Add tasks** button in the steps section

 ![](<media/add_build_tasks.png>)

 > You can see that it's very easy to deploy to a resource on Azure. In the cloud or On-Premises you have access to several tasks
 > to help you in your deployment here. In MRP, we use PowerShell to perform actions via SSH on the Linux machine

0. Click **Add** for tasks **Copy Files Over SSH** & **SSH**

 ![](<media/add_ssh_tasks.png>)

 [Show the options in the copy files to the remote machine task]

 [Show the options in the Run Shell command task]

0. Remove tasks **Copy Files Over SSH** & **SSH**

0. Select the **Deploy MRP using sftp and SSH through PowerShell** task

 ![](<media/remove_ssh_tasks_select_powershell.png>)

 [Open the type dropdown and explain that you can run an inline script file that already exists.
  We are using a powershell script contained in the deploy package that come out of the CI build]

0. Browse to **Build** tab

 ![](<media/browse_build_tab.png>)

0. Click the **PartsUnlimitedMRPCI** build and navigate to the last successful CI build.

 ![](<media/partsunlimitedmrpci_build.png>)

0. Click the **Artifacts** tab in your build detail

0. Download the **deploy** artifacts 
 
 ![](<media/ci_artifacts_download.png>)

0. Open the **deploy.zip** file

0. Open the **SSH-MRP-Artifacts.ps1** file in VS Code

 [Explain that two files are downloaded to help us transfer files and communicate in SSH (psftp.exe & plink.exe)]

 [Explain that deploy files are going to be transferred through SSH]

 [Explain that drop  files are going to be transferred through SSH]

 [Explain that the sftp command use one of the out of the box variables that the build agent expose, 
  in our case $env:BUILD_DEFINITIONNAME]

 [Explain that a shell script is created that invokes dos2unix to ensure the 
  shell script is compatible on the Linux machine]

0. Open **deploy_mrp_app.sh** from the zip file

 [Explain that this shell script is the actual deployment script for Parts Unlimited MRP]

 [Briefly list the one-liner comments that states what the script does]

## Teardown
None.  

## Complementary information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)