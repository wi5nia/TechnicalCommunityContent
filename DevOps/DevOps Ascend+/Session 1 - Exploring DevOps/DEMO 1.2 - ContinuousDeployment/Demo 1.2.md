# DEMO 2 - Parts Unlimited MRP - App Continuous Deployment on multiple environments with Visual Studio Team Services - Hosted #

### Duration
This demo should take ~ 12 minutes

### Goal
Demonstrate a step through from taking a change request all the way to a release in production using some of the DevOps principals along the way.

### Prerequisites
* An active Visual Studio Team Services (VSTS) account
* [SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted](../../SETUP_Continuous-Deployment/Setup.md)
* An Internet connection
* [Visual Studio CODE](http://code.visualstudio.com/)
* Git for Windows or Linux

**NOTE:** If you are running Linux on your local workstation, install git:

    sudo apt-get install git
 
**NOTE:** If you are running Windows, you have to install the git client from here:

[http://git-scm.com/download](https://git-scm.com/download/win)

## Demo

 We will demonstrate how some of the DevOps principles can help ensure we deliver more 
 business value while minimizing the risks of the wrong code being pushed to production.
 

## Create feature branch in VSTS
Browse to `https://<your-vsts-account>.visualstudio.com/PartsUnlimitedMRP`

> You start your day by going into your tasks and issues tracking to pickup an item to work on. You see that a change request is there and you decide to pick it up.

* At the homepage of the PartsUnlimitedMRP team project in Visual Studio Team Services, click on the **Work** tab in the upper-left corner of the page.
 
> To do so, you will make your changes in a new branch to isolate your changes. It is a good practice to do so; 
 that way you can enforce pull requests and perform code reviews before you merge your changes back in your source branch.

* Click the ellipsis on the right of the work item titled *Change Clients web site titles* and choose **New Branch...**.
 
 ![](<media/new_branch_on_work_item.png>)

* Enter `feature/title-change` for the Name and click the **Create branch** button.

 ![](<media/create_feature_branch_dialog.png>)

* In the **CODE** tab, switch to `feature/title-change` branch, click the **Clone** button and **copy the Clone URL**.

![](<media/get_git_url_for_repo.png>)

## Clone your repository on your local machine

> We will now clone the repository on our machine using the clone url we just copied.

*In a command prompt*, create a directory for our repository

Clone the **PartsUnlimitedMRP** git repository onto your local machine, open your command-line tool and type :

    `git clone https://<your-vsts-account>.visualstudio.com/_git/PartsUnlimitedMRP pumrp`
 
**[this can take up to a minute or two depending on your Internet connection]**

## Work in Visual Studio CODE
* Open up Visual Studio code

> Visual Studio CODE is multi-platform, open source editor. We will come back to it a bit later when the build process is busy building our changes.

* Open the repository folder containing the copy of the repository you just cloned
 
 ![](<media/open_repo_folder_vscode.png>)

> We will switch to the feature/title-change

* Press the **F1** key and type *git checkout*, then choose the **feature/title-change** branch and press **Enter**. 

 ![](<media/checkout_feature_branch.png>)

* Browse to `src/Clients/Web/index.html`, then modify the title to *Parts Unlimited - Beta*. 

 ![](<media/edit_index_webpage.png>)
 
* Save the file.

* Browse to the file `src/Clients/Web/pages/main/main.html`.

* Change the **pageTitle** span element to *Welcome to Parts Unlimited MRP system - Beta*

  ![](<media/vscode_edit_main_page.png>)

* Save the file.

> Now that we have complete our change request, we need to commit locally in order to be able to push the changes back to the remote repository.

*  Click the **Git** tab on the left-nav bar, enter the message `Title change`, then click the **checkmark** to commit.

 ![](<media/commit_changes_locally.png>)

*  Click the **ellipsis**, then click **Push** to commit.

 ![](<media/push_to_remote.png>)

> Now that the changes are pushed to our remote branch we can create a Pull Request. Like we said earlier, working on the master or your main branch directly is not a good idea.

> We will create a pull request for this change in VSTS.

*  Go back to VSTS and browse the **CODE** tab; ensure you are on the `feature/title-change` branch, then click **Create a pull request**.

 ![](<media/create_pull_request.png>)

> This is where you could set one or many specific reviewers or leave it by default which anyone on your team can review the Pull Request.

> You could also change the PR Title, description, and assign work items to it.

* Ensure the direction is `feature/title-change` to `master` and press the **New Pull request** button.

 ![](<media/new_pull_request_dialog.png>)

> For the sake of the demo and because we are not many people, we will approve and complete the PR ourselves. In real life, one or more people 
on your team would do this.

* Click the **Approve** button, then the **Complete** button.

 ![](<media/approve_complete_pr.png>)

* Click the **Complete merge** button.

 ![](<media/complete_pr.png>)

* Go to the **Build** tab and show the running build (In progress) that was queued automatically by the *Continuous Integration* trigger. 

 ![](<media/ci_build_triggered_by_pr.png>)

**[the build will take ~3 minutes to complete]**

* Go back to VS CODE and to show the tool around

 ![](<media/vscode_overview.png>)

	Explorer:	Provides you the ability to navigate and open files and folders
  
	Search:		Enable you to perform text search on files using simple pattern or regular expressions
  
	Git:		Enables you to perform several Git related commands; we already saw that when we performed a commit & push
     			but you can, perform pull, rebase, undo last commit, etc.

	Debug:		Like the name says, you can debug JavaScript, node.js and many other things in there

	Extensions:	VS CODE is a great tool that has a lot of extensions for things that are not out of the box.
     This is where you will install, enable, disable, uninstall your extensions. 
     An example of extensions you can find is the Azure Resource Manager Template extension 
     which gives you intellisense when you edit your JSON files as part of your Infrastructure as Code (IaC) preparation.

* Go back to VSTS and show progress of the build

> The Continuous deployment & Release management principles of DevOps is done in the **Release** tab in VSTS

* Go to the **Release** tab and note the running deployment that was queued by the Continuous Deployment trigger. 

  ![](<media/cd_deployed_to_dev.png>)

> All the tracking about your release that you can expect is right there in front of you... 
> In the *Details* section, you can see the details of what triggered the CD build, by who, at what time and from which branch.
> Also in the *Work Items* section, you can see the origin of this release and who worked on that very specific feature/bug.

> After a couple of minutes the development environment will be refreshed with the new release.
> We'll verify just that, that our code changes were deployed in the **DEV** environment

* Browse to [https:://portal.azure.com](https:://portal.azure.com)

* Browse to the Azure portal in your dev resource group, then click the Linux VM 

  ![](<media/browse_dev_rg_azure_portal.png>)

* Copy the DNS name of your Linux machine

  ![](<media/azure_portal_copy_dev_machine_dns.png>)


> The title of the page has changed and we are satisfied but let's pretend for one minute that it was not the case...
> The approver always has the option to stop the propagation of a release to other environments by *rejecting* the release. 

* Paste what you have copied into the address bar of a new browser window. Keep only the DNS name, not the IP address if it is present, at the end; the URL 
should look like something like this: `http://mylinuxvm-dev.westus.cloudapp.azure.com:9080/mrp`.

 ![](<media/release_deployed_to_dev_env.png>)

> In our case here, we are very satisfied and the approver (us) will allow the release to continue to the next environment.

* Go back to VSTS in the CD build

* Click the **clock** icon next to the NOT DEPLOYED label for the production environment, then click the **(Override)** link.

  ![](<media/override_cd_prod_environment.png>)

* Click the **Approve** button.

  ![](<media/cd_override_dialog_approve.png>)

> This informs Release Management that our release can go the next environment, in our case, **PROD**

**[This will take around a minute to deploy]**

* Show the status change for the production environment.

* Verify your code change by navigating to the **PROD** VM's public IP DNS name, such as `http://mylinuxvm-prod.westus.cloudapp.azure.com:9080/mrp`.

**[TIP: copy the dev environment URL in the open browser and replace *-dev* for *-prod* in the host name ]**

## Teardown
If you don't intend to perform another session in this series, you can delete the resources for the dev & prod environments in Azure. Execute `/docs/Sessions/SETUP_Continuous-Deployment/env/teardown.ps1` script from the setup folder.  

Navigate to the admin section of your VSTS account: `https://{VSTS instance name}.visualstudio.com/_admin`

Right-click the ellipsis button, the click ***Delete*

Confirm the name for deletion

## Complementary Information

* [DevOps Fundamentals](https://channel9.msdn.com/Series/DevOps-Fundamentals)