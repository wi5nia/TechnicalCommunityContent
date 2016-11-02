# Demo 3 Parts Unlimited MRP - Automated Testing

### Duration
This demo should take ~ 5 minutes

### Goal
Introduction and high-level overview of the Automated Testing DevOps principal in Parts Unlimited MRP. 

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* [SETUP - Parts Unlimited MRP App Automated Load Testing with Visual Studio Team Services](../../SETUP_Automated-Load-Testing/Setup.md)
* An Internet connection

# Demo

## Explain automated testing using JUnit unit tests

0. Browse to VSTS project page for Parts Unlimited MRP at `http://<YourVSTSAccount>.visualstudio.com/PartsUnlimitedMRP` and navigation to the **build** page:

    ![](<media/browse_build_page.png>)

0. Click the **Edit...** option on the MRP build definition :

    ![](<media/browse_build_definition.png>)

0. Click on the **OrderService** build step to show it's options :

    > Automated testing can take namy shapes and forms and can be integrated into different DevOps aspects.  For example, unit testing can be run during the build in order to
    > ensure code quality and standards early in the process.  In the scope of Parts Unlimited MRP, **JUnit** unit tests are executed on the _OrderService_ Gradle Wrapper and published to
    > VSTS for results:

    ![](<media/junit_build_step.png>)

0. Click on the **Explorer** tab:

    ![](<media/browse_build_explorer.png>)

0. Open a **successful build** to view the results:

    ![](<media/browse_build_results.png>)

0. Click on the **Tests** tab to view detailed test results:

    ![](<media/browse_test_results.png>)

0. Show the **detailed test results**: 

    ![](<media/test_results.png>)

## Show the load test automation

0. Browse to VSTS project page for Parts Unlimited MRP at `http://<YourVSTSAccount>.visualstudio.com/PartsUnlimitedMRP` and navigate to the **release** page:

    ![](<media/browse_release_page.png>)

0. Click on the **Edit** link of the _PartsUnilimitedMRPCD_ release definition:

    ![](<media/edit_release_definition.png>)

0. Make sure the _DEV_ environment and select the **Create Load Test Configuration** task:

    > Since load tests are usually a long process that are executed on a provisioned environment, it makes sense to incorporate them in the release management phase of the
    > DevOps cycle.  We can imagine a scenario where load tests are executed on the latest provisioned _DEV_ environment to make sure performance is adequate before
    > approving the release for the _PROD_ environment.  Here, a **PowerShell** task creates a **JMeter load test file (\*.jmx)** for a specific cloud app environment (ex: _pumrp-test-1234-dev.westus_).  It's easily
    > configurable for the _PROD_ environment if need be:

    ![](<media/browse_ps_task.png>)

0. Select the **Apache JMeter Test MRP\_load\_test.jmx** task:

    > Here a VSTS endpoint is defined to be able to provision load test agents automatically for the load test.  A simple **\*.jmx** file is required in the configuration.  You can
    > also adjust the number of agents required to execute the tests depending on the load and also the total duration of the tests.  Note that the requests, wait times and number of
    > concurrent users are defined in the jmx file. 

    ![](<media/browse_loadtest_task.png>)

## Show example load test results

0. Browse to the **Test** page:

![](<media/browse_test.png>)

0. Click on the **Load test** tab:

![](<media/browse_loadtest.png>)

0. Open the latest completed MRP load test:

    > Load test results are always visible through the **Load test** tab.  Load tests can also be run directly through the portal.
    > Charts and summary data can be viewed by selecting a single load test result.

![](<media/open_loadtest.png>)

0. Browse the load test details page: 

    ![](<media/loadtest_details.png>)

## Teardown
0. **Disable** the **Create Load Test Configuration** & **Apache JMeter Test MRP_load_test.jmx** tasks in the **Dev** environment
0. Save the release definition

## Complementary information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)