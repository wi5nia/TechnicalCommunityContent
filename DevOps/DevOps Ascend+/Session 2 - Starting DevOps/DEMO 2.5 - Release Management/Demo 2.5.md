# Demo 5 Parts Unlimited MRP - Release Management

### Duration
This demo should take ~ 8 minutes

### Goal
Introduction and high-level overview of the Release Management DevOps principal in Parts Unlimited MRP. 

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* An Internet connection

# Demo

## Explain the release management functionalities

0. Browse to **Release** tab

 ![](<media/browse_release_tab.png>)

 > In an Agile world, delivering quick and frequent releases for large, complex systems with multiple components becomes 
 > cumbersome and time-consuming if done manually, because each component has a high degree of complexity and requires 
 > a lot of resource intervention and configuration to ensure that it works as expected.
 >
 > That's why many teams opt for Build and Deployment Automation... to ensure faster releases and reduce manual intervention. 
 > However, automating multiple components of a system has its own challenges. Even though releases can be automated in silos,
 > if we need a one-click deployment for the entire  system, we need to have an automation framework that can automate an entire custom workflow.
 >
 > In Release Management, you deal with "Releases" and a "Release" will go from environment
 > to another environment with /or without approval depending on how your configured your release pipeline.

0. Right-click **PartsUnlimitedMRPCD** build and click **Edit**

 > In the case of PartsUnlimitedMRP, there are only two environements, **Dev** & **Prod**. But in your release pipeline you
 > could have more environments than this... We can easily think about QA, Staging, UAT, etc..
 > 
 > Environments do not need to have the same tasks defined, for example, for production, you can have more tasks. 
 > It is the right place to run tests other than unit tests like Integration, UI, Performance, Load tests 
 >
 > In **Dev** for MRP, we have four (4) tasks compared to only two (2) in **Prod** 

 ![](<media/edit_pumrpcd_build.png>)

0. Right-click the **Prod** environment and click **Approvals**

 ![](<media/assign_approvers.png>)

 ![](<media/prod_approvals.png>)

 > You can have pre-deployment & post-deployment approvals in environments. Doing so you'll probably want
 > to define a user or group as the approver.

0. Expand the More **Options link** under *Pre-deployment Approver*
 
 > You can also specify the number and order of approvals you need. 
 > You can have All users in any order, All users in sequential order or Any one user 
 >
 > In the options at the bottom, you'll most likely want to enable email notification 
 > for the approver whom approval is pending on

0. ENSURE **Send an email notification to the approver whom the approval is pending on** is checked

0. Click the **Variables** tab

 ![](<media/prod_variables.png>)

 [Explain that you define variables that are specific to an environment in here and 
  that they can later be referenced inside build tasks]

0. Click the **Deployment conditions** tab

 ![](<media/prod_deployment_conditions.png>)

 > In the first environment (Dev), the deployment is automatic as soon as a *Release* is created but not in *Prod*.
 > In *Prod* we want to trigger the deployment only after it was successful in *Dev*

0. Click the **General** tab

 ![](<media/prod_deployment_conditions.png>)

 > You can configure email notification for the environment owner & release creator. 
 > You decide on WHICH event and WHO will receive email notifications

0. Click **OK** to save the changes if you made modification to the **Send an email notification to the approver whom the approval is pending on** checkbox
0. [Optional] Click **Save** to save your release definition.

0. Click the **Triggers** tab of *PartsUnlimitedMRPCD* definition

 ![](<media/cd_tiggers_tab.png>)

 > Triggers is what enable this build to be a Continuous Deployment build. It's a CD build that makes the 
 > Release pipeline possible in MRP. 
 > 
 > It this case, it will trigger when the *PartsUnlimitedMRPCI* build produces new artifacts

0. Click the **Environments** of *PartsUnlimitedMRPCD* definition

0. Click the **Release/+ Create Release** menu

 ![](<media/create_release_from_definition.png>)

0. Choose the latest CI build in dropdown and click **Create**

 ![](<media/create_new_release_manual.png>)

0. Click the new release link we've just created

 ![](<media/new_manual_release_link.png>)

0. Click the **Commits** tab

0. Click a commit and open the link in a new tab

 ![](<media/release_commits_tab.png>)

 > You have the ability in this tab to see what commits were introduced between two releases.
 > 
 > You can dig all the way through and go see commits details directly from a Release, this is what we can call traceability!

0. Click the **Work Items** tab and choose an older release and click the **Compare** button

 [Explain that again you have great traceability and can see what work items are associated in a release compared to an older one]

0. Click the **Summary** tab

 ![](<media/new_manual_release_summary.png>)

0. Click the **Abandon** button

 > You can Abandon a release at any point in time and doing so, no further actions are possible on that release

0. Click the **Send Email** button

 [Explain that you can send an email with release details to other people]

 [ !!! Most likely the deployment to the *Dev* environment will not be completed yet, 
  if it's the case, approve an older release to *Prod* environment]

0. Approve a release to **Prod**.

 ![](<media/approve_existing_release_to_prod.png>)

0. Click the **Approve** button

 ![](<media/override_approve_prod_release.png>)

 [Explain that the release will now reach production]

## Teardown
None.  

## Complementary information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)