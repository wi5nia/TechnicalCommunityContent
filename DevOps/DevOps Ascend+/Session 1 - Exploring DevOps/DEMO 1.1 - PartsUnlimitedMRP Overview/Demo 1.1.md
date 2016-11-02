# Demo 1 Parts Unlimited MRP - Overview

### Duration
This demo should take ~ 5 minutes

### Goal
Introduction and high-level overview of the components used and how they relate to the 7 core DevOps principals. 

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* An Internet connection

# Demo

## Explain what Parts Unlimited is

0.  Browse to the Parts Unlimited MRP site in the dev environment: `http://{YourDNSname}:9080/mrp`
The MRP Url was shown in the output of the script used in the setup process.

    > Parts Unlimited is a fictional organization that sells car parts online. Their business relies on two key applications: a new ASP.NET vNext e-commerce website and an Java-based outsourced Manufacturing Resource Planning (MRP) application. The scenario and e-commerce site is based on the website described in chapters 31-35 of The Phoenix Project, by Gene Kim, Kevin Behr and George Spafford, © 2013 IT Revolution Press LLC, Portland, OR. Resemblance to “Project Unicorn” in the novel is intentional; resemblance to any real company is purely coincidental.

    > Today we will use the MRP application to show you how DevOps can help you deliver value & your code with more confidence.

    > The application is written in Java and runs on a Linux machine hosted on Microsoft Azure. Let's take a look at its infrastructure in the cloud.

0. Open the Microsoft Azure portal at https://portal.azure.com
0. Browse to your Azure subscription and browse to the **PartsUnlimitedMRP-dev** resource group

    ![](<media/browse_dev_rg_azure_portal.png>)
0. In the overview pane, show the Linux Virtual machine

    > The Linux machine is the host of a few components - MongoDB for the data & Apache Tomcat for the web.

0. Open the folder .\src in your local repository and explain the folder structure

    ./src/Backend/IntegrationService

    > The Integration Service, written in Java and output as a .jar file

     ./src/Backend/OrderService

    > The Order service, written in Java and output as a .jar file

    ./src/Clients/Web

    > The MRP client is a static website. The build process here creates a single archive (war) file that is used by the Tomcat host for the site.

## Open your Parts Unlimited MRP VSTS project homepage
Browse to `https://<your-vsts-account>.visualstudio.com/PartsUnlimitedMRP`

## Explain how DevOps principles match in the case of Parts Unlimited MRP

### Infrastructure as Code & Configuration Management
Open the file `./docs/Sessions/SETUP_Continuous-Deployment/env/ContinuousDeploymentPartsUnlimitedMRP.json`

    Infrastructure as Code (IaC) is the process of managing and provisioning computing infrastructure 
    (processes, bare-metal servers, virtual servers, etc.) and their configuration through 
    machine-processable definition files

    In MRP, IaC is done using an Azure Resource Manager template file (JSON format) that contains 
    the desired state of resources to be created in Azure. During provisioning there is a VM extension
    that will configure the machine by installing, without human intervention,
    Apache Tomcat & MongoDB just to name a few.

#### Other well-known IaC tools:
* Chef
* Puppet
* Terraform
* Ansible
* Salt
   
-------------------------------------------------------------------------------
### Continuous Integration
Click the **Build** tab in VSTS

    A continuous integration build will give us the ability to automate whether the code
    we checked in can compile and whether it successfully passes any automated tests that we
    have created against it. By using an automated build pipeline, we can quickly validate if 
    our code changes have "broken the build" and fix code before it gets to production.

    In MRP, the CI build is triggered automatically when code is pushed to the source control.
<!--
    ![](<media/browse_to_ci_build_properties.png>)

    ![](<media/ci_build_trigger.png>)
-->
#### Other well-known build/CI tools:
* Jenkins
* Travis CI
* Team City
* Cruise Control

-------------------------------------------------------------------------------
### Automated Testing // aka Continuous Testing
Click the **Build** tab in VSTS

    Visual Studio Team Services includes build tasks to make it easy to integrate your build and 
    testing efforts into your release pipelines with rich reporting highlighting build-on-build changes,
    easy-to-create actionable bugs for regressions and the ability to create and display code coverage 
    with a simple checkbox.

    In MRP, Automated Testing is performed by executing JUnit tests during the CI build but you could go 
    way further than that by executing UI tests, load tests, etc.

<!--    
    VSTS Build Definition executing *JUnit Tests* - [more doc here](https://www.visualstudio.com/en-us/docs/test/continuous-testing/getting-started/continuous-test-java)

    ![](<media/automated_testing.png>)
-->
#### Other well-known automated testing tools:
* Selenium
* Cucumber
* JUnit 
* JMeter
* XL TestView

-------------------------------------------------------------------------------
### Continuous Deployment & Release Management
Click the **Release** tab in VSTS

    Continuous Deployment & Release Management help you automate the deployment and testing of 
    your software in multiple environments. You can either fully automate the delivery of your 
    software all the way to production, or set up semi-automated processes with approval 
    and on-demand deployments.

    It is an essential element of DevOps that helps your team continuously deliver software 
    to your customers at a faster pace and with lower risk.
    
    In MRP,
        Continuous Deployment is done using a deployment build with a trigger set on a CI build.
    
        Release Management is achieved using the release tracking & the approval that exists for 
        the deployment between the dev & production environments.
<!--
    ![](<media/browse_to_cd_build_properties.png>)

    ![](<media/cd_build_trigger.png>)
!-->

<!--
    ![](<media/release_management_approvals.png>)

    *Deployment conditions* in Release Management

    ![](<media/release_management_deployment_conditions.png>)
-->

#### Other well-known CD tools:
* Jenkins
* Octopus Deploy
* Go
* Bamboo
* UrbanCode Deploy
* Team City

#### Other well-known Release Management tools:
* Octopus Deploy
* Apache Continuum
* UrbanCode Release 

-------------------------------------------------------------------------------
### Application Performance Monitoring / Management

    Application Performance Monitoring is the monitoring and management of performance and 
    availability of software applications. APM strives to detect and diagnose complex 
    application performance problems to maintain an expected level of service.
    
    In MRP, APM is done using Application Insights. Application Insights is an extensible analytic
    service that helps you understand the performance and usage of your live application.
    It is used to detect and diagnose performance issues and exceptions, and write code to track 
    what users do with your app.
<!--
    * Application Insight integrated with Java - [more doc here](https://azure.microsoft.com/en-us/documentation/articles/app-insights-java-get-started/)
-->
#### Other well-known APM tools:
* New Relic
* Splunk


## Teardown
You’ll be reusing the same services in this session's next demo, so don’t delete them yet.  

## Complementary information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)