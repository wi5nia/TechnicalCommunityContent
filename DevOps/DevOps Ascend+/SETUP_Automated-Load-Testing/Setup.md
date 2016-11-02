# SETUP - Parts Unlimited MRP App Automated Load Testing with Visual Studio Team Services #

### Prerequisites ###

- An active Visual Studio Team Services (VSTS) account
- Project Collection Administrator rights to the Visual Studio Team Services account
- An active Azure Subscription

---
### Tasks Overview ###

In this setup, you will perform the following:

**Create a VS Team Service Endpoint:**

This step has you create a service endpoint which you will use in your automated load test.

**Create and configure an Apache JMeter load test:**

In this step, you will configure an Apache JMeter load test in the _DEV_ environment's continuous deployment steps.

**Execute a load test:**

In this optional step, you will execute a continuous deployment to be able to show load test results in demos.

-----------------------------------------------------------------------------------
## Task# 1: Create a VS Team Service Endpoint

**1.** First, navigate to the Parts Unlimited MRP project in your Visual Studio Team Services account by typing into a browser: 

    https://{VSTS instance name}.visualstudio.com/PartsUnlimitedMRP

**2.** Once at the project home page, navigate to the settings page by clicking on the **upper-right gear**:

![](<media/browse_project_settings.png>)

**3.** Navigate to the services configuration page by click the **services tab**:. 

![](<media/browse_project_services.png>)

**4.** Create a new service endpoint by clicking on the **new service endpoint** button and then selecting the **generic service** option: 

![](<media/create_service_endpoint.png>)

**5.** In the **Add new Generic Connection** screen, configure the service endpoint with the following settings and **Click OK**:

* **Connection Name:** VSTS
* **Server URL:** https://{VSTS instance name}.visualstudio.com
* **User Name:** {your user name}
* **Password/Token Key:** {your password or personal access token}

-----------------------------------------------------------------------------------
## Task# 2: Create and configure an Apache JMeter load test:

**1.** Navigate to the Parts Unlimited MRP project in your Visual Studio Team Services account by typing into a browser: 

    https://{VSTS instance name}.visualstudio.com/PartsUnlimitedMRP

**2.** Navigate to the build page by clicking the **Build** tab:

![](<media/browse_build_page.png>)

**3.** Edit the _PartsUnlimitedMRPCI_ **Build Definition**:

![](<media/edit_build_definition.png>)

**4.** Add a **Copy and Publish Build Artifacts** build step to the end of the existing steps:

![](<media/add_publish_step.png>)

**4.** Edit the **Copy and Publish Build Artifacts** step with the following configuration:

* **Copy Root:** $(Build.SourcesDirectory)
* **Contents:** \*\*/docs/Sessions/SETUP_Automated-Load-Testing/env/\*.\*
* **Artifact Name:** load-test
* **Artifact Type:** Server

![](<media/configure_publish_step.png>)

**5.** Save the build definition:

![](<media/save_build_defintion.png>)

**6.** Navigate to the release management page by clicking the **Release** tab:

![](<media/browse_release_page.png>)

**7.** Edit the _PartsUnlimitedMRPCD_ **Release Definition**:

![](<media/edit_release_definition.png>)

**8.** In the **DEV** environment, add a _PowerShell_ and a _JMeter Load Test_ task (in the specified order):

![](<media/add_ps_task.png>)
![](<media/add_jmeter_task.png>)

**9.** Edit the **PowerShell Script** task with the following configuration:

* **(Task name):**    Create Load Test Configuration
* **Type:** File Path
* **Script Path:** `$(System.DefaultWorkingDirectory)/PartsUnlimitedMRPCI/load-test/Create-LoadTestConfigurationFromTemplate.ps1`
* **Arguments:** `-WorkingDirectory "$(System.DefaultWorkingDirectory)\PartsUnlimitedMRPCI\load-test" -CloudAppName $(sshTarget)`

**Note: Replace _{cloud_app_name}_ with your development cloud app name (ex: pumrp-test-1234-dev.westus)**

![](<media/config_ps_task.png>)

**10.** Edit the **Apache JMeter** task with the following configuration:

* **VS Team Service Endpoint:** VSTS
* **Apache JMeter test files folder:** `$(System.DefaultWorkingDirectory)/PartsUnlimitedMRPCI/load-test`
* **Apache JMeter file:** `MRP_load_test.jmx`
* **Agent Count:** 1
* **Run duration (sec):** 60

![](<media/config_jmeter_task.png>)

**11.** Save the release definition:

![](<media/save_release_definition.png>)

-----------------------------------------------------------------------------------
## Task# 3: Execute a load test:

**Note: Make sure your development cloud app is up and running before executing the load test**

**1.** Queue a new build:

![](<media/queue_build.png>)

**2.** Wait for the end of the build and for the development release to finish:

![](<media/browse_releases.png>)

[The load test should be executed in the latest release and be ready for demonstration purposes]