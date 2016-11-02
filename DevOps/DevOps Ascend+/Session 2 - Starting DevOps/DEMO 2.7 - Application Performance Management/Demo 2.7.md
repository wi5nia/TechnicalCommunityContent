# Demo 7 Parts Unlimited MRP - Application Performance Management

### Duration
This demo should take ~ 7 minutes

### Goal
Introduction and high-level overview of the Application Performance Management DevOps principal in Parts Unlimited MRP. 

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* An Internet connection
* **Disable the *Create Load Test Configuration* & *Apache JMeter Test MRP_load_test.jmx* tasks from the *Dev* environment in the *PartsUmlimitedMRPCD* build**  

# Demo

## Explain performance problem troubleshooting

0. In your internet browser, browse to Parts Unlimited MRP *DEV* environment - `http://<YourDNSname>:9080/mrp`

0. Click around and then on **Dealers**

 ![](<media/browse_mrp_around.png>)

 [Explain that there is presently a performance problem and that this page is very slow to load]

 ![](<media/long_dealers_loading.png>)

 [Explain that since we do not have a lot of time for the demo we will immediately modify the code that causes this to start a new build/deployment 
  and we'll check out how APM is setup in the meantime]

0. Navigate to the working folders of your PartsUnlimitedMRP repo in a code editor (such as VSCode).

 > We know that this performance problem happens when we ask for the dealers so this must come from
 > the dealers controller or the dealers repository 

0. Find the `getDealers()` method in `./src/Backend/OrderService/src/main/java/smpl/ordering/controllers/DealerController.java` that is causing slow performance.

 ![](<media/find_getdealers_in_code.png>)

 [In the `getDealers()` method]

 ![](<media/get_dealers_loop_problem.png>)
 
 > Notice that when a request is made to get the dealers, the code loop several thousand times into MongoDB 
 >
 > Usually you would have gone into your APM tool and identify problems from there, we'll see in a a minute
 > that APM would have help us spot this problem right away
 
0. Change the value of the **numMongoDBCalls** variable to 1
 
 > Changing this should really solve our performance problem for the dealers section!

0. Save the changes and commit the changes on the master branch.

0. Push the changes to the remote repo in VSTS to kick off a Continuous Integration build.

 ![](<media/commit_push_performance_fix.png>)

 [This will take between 3-5 minutes, meanwhile we'll show how Application Insights is integrated in build]

 > Now this will take some time since it will trigger the CI build, then the CD build onto the *Dev* environment.
 > Let's briefly see  what we can see in APM and what would have helped us identify the problem in the first place...

0. In a new browser tab, browse to the Azure portal into your dev environment, open the **Application insights resource**

 ![](<media/browse_to_app_insights.png>)

 ![](<media/browse_app_insights_performance_tile.png>)

 > There is a lot of available information in there, We have access to Server response times, Page View load time, Server requests and Failed requests.
 >
 > We can see that there are peeks for page load time, let's take a look at the *Performance* tile in Application Insights.

0. Scroll down and click the “Performance” tile to view performance monitoring information

 [Explain under **Average of Dependency duration by Remote dependency name** there is an abnormal number of calls to MongoDB, maybe this is related...]

 > We can see that there is a problem in there... The number of calls to MongoDB.FindAll is way too high compared to 
 > the number of page requests that have.

0. Click the “Failure” tile to have a view over failure

 ![](<media/app_insights_failure_tile_details.png>)

 > You can also trace failures or error if you want. You really have a lot of details around failures at web app level or server level
 > depending on how you configure your environment & code. In the MRP application, only code is instrumented.
 > 
 > You can click on errors that interest you to get all the occurrence of that error. Doing so enable you to view 
 > a single occurrence of this error and get details about that specific event.
 >
 > Let's take a look at how instrumentation is done in Parts Unlimited MRP

0. Browse to VSTS project page for Parts Unlimited MRP at `http://<YourVSTSAccount>.visualstudio.com/PartsUnlimitedMRP`

0. Click the **Release** tab

 ![](<media/browse_release_tab.png>)

0. Right-click the **PartsUnlimitedMRPCD** CD build and click **Edit**

 ![](<media/edit_pumrpcd_build.png>)

0. Click the **Dev** environment and click the **Configure APM** task

 ![](<media/browse_to_configure_apm_task.png>)

0. Open the file `.\deploy\Set-APMInDrop.ps1` in Visual Studio Code

 ![](<media/setapmindrop_script_in_vscode.png>)

 > This script will extract java packages from the *drop* artifacts that are present when the CD build executes.

 [Explain all the steps that are performed by the script - comments are present in file]

 > Working this way enables MRP to have more than one Application Insights setup (one per environment) using the same release package

 [Check back in VSTS to confirm the deployment on the *Dev* environment is completed]

0. Navigate around in the application and return to the **Dealers** section. 

 [The dealers will show up faster than they did previously now having one call to the database.]

## Teardown
If you don't intend to perform another session in this series, you can delete the resources for the dev & prod environments in Azure. Execute `/docs/Sessions/SETUP_Continuous-Deployment/env/teardown.ps1` script from the setup folder.  

Navigate to the admin section of your VSTS account: `https://{VSTS instance name}.visualstudio.com/_admin`

Right-click the ellipsis button, the click ***Delete*

Confirm the name for deletion

## Complementary information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)

* [Get started with Application Insights in a Java web project](https://azure.microsoft.com/en-us/documentation/articles/app-insights-java-get-started/)

* [Unix performance metrics in Application Insights](https://azure.microsoft.com/en-us/documentation/articles/app-insights-java-collectd/)

* [Application Insights API for custom events and metrics](https://azure.microsoft.com/en-us/documentation/articles/app-insights-web-track-usage-custom-events-metrics/)
