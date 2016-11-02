 # Demo 2 Parts Unlimited MRP - Continuous Integration

### Duration
This demo should take ~ 5 minutes

### Goal
Introduction and high-level overview of the Continuous Integration DevOps principal in Parts Unlimited MRP. 

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* An Internet connection

# Demo

## Explain the build functionalities

0. Browse to the VSTS project page for Parts Unlimited MRP at `http://<YourVSTSAccount>.visualstudio.com/PartsUnlimitedMRP` and navigation to the **build** page:

    ![](<media/browse_build_page.png>)

0. Click the **Edit...** option on the MRP build definition :

    ![](<media/browse_build_definition.png>)

0. Click on the **IntegrationService** build step to show its options :

    > The build is defined through **build steps**.  Build steps provide the functionalities to build, test and package the application.
    For example, MRP is a Java application and thus each service is built using the Gradle wrapper script:

    ![](<media/browse_gradle_buildstep.png>)

0. The build artifacts are then published to the server using a **Copy and Publish Build Artifacts** build step:

    ![](<media/browse_publish_buildstep.png>)

0. Click **Add build step...** to show what other build steps are available.

 [Explain there are various other build steps to choose from, a lot of open source options are present]    

## Show the available build definition options

0. Click on the **Options** tab:

    ![](<media/browse_options_tab.png>)
    
     > Some advanced options are available but out of the scope of Parts Unlimited MRP.  For example, multiple build configurations can be run in parallel
     > depending on configuration variables (ex: x86 and x64 platforms).  Another example is being able to create a work item on build failure (ex: fix broken build):

    ![](<media/browse_options.png>)

## Explain the repository settings

0. Click on the **Repository** tab:

    ![](<media/browse_repository_tab.png>)

     > Repository types range from **Git** version control to **Subversion**.  Connecting the source code is done by selecting the repository and branch.
     > It's also possible to clean the workspace before initiating a build, use submodules (Git only) and report the build status to the developers.

    ![](<media/browse_repository.png>)

## Explain the build triggers

0. Click on the **Triggers** tab:

    ![](<media/browse_triggers_tab.png>)

    > Builds can be triggered in two different ways:
    >
    > * On each checkin (batch or single commits)
    > * Scheduled times (weekly reccurence)

![](<media/browse_triggers.png>)

## Show the build definition general settings

0. Click on the **General** tab:

    ![](<media/browse_general_tab.png>)

    > General settings include which agent queue to use, 
    > who is authorized to trigger a build job and other miscellanious configurations.
    > For example, the default agent queue used in MRP is the **Hosted** agent queue.  Hosted agents
    > are provided with a [rich set of tooling already installed](https://www.visualstudio.com/docs/build/admin/agents/hosted-pool#software)
    > to support as many builds possible.
    >
    > Here are a few capabilities installed out of the box on a hosted build agent
     >
     > * Multiple versions of .NET, from 3.5 to 4.6+
     > * .NET Core 1.0
     > * Multiple versions of the Microsoft Azure SDK
     > * Android Software Development Kit
     > * Apache ANT
     > * Apache Maven
     > * CMAKE
     > * Java JDK
     > * Node.js
     > * Python    
     > * Typescript
     > * Xamarin    
     > * etc...
     >
    > It' also possible to **specify a single or multiple demand(s)** that the agent must meet in order to the build.  For example, MRP
    > runs in Java so it is possible ensure the that java exists on the agent.

    ![](<media/browse_general.png>)

## Teardown
None.  

## Complementary information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)    