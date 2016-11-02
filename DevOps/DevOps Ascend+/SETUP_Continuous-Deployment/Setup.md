# SETUP - Parts Unlimited MRP App Continuous Deployment on Multiple Environment with Visual Studio Team Services - Hosted #

### <a name="prerequisites"></a>Prerequisites ###

- An active Visual Studio Team Services (VSTS) account
- Project Collection Administrator rights to the Visual Studio Team Services account
- An active Azure Subscription
- [Azure PowerShell](https://github.com/Azure/azure-powershell/releases/tag/v2.2.0-September2016)
- Git

### Tasks Overview ###

In this setup, you will perform the following:

**Set up your Visual Studio Team Services account**

This step helps you download the source code, and then push it to your own Visual Studio Team Services account.

**Create a Continuous Integration Build**

In this step, you will create a build definition in Visual Studio Team Services that will be triggered every time a commit is pushed to your repository in Visual Studio Team Services. 

**Provision the Dev & Prod environment**
    
Provision the MRP machines (Ubuntu VM) in Azure using an ARM template & PowerShell script.

**Configure the release definition**

Configure the release definition in VSTS that picks up build artifacts and triggers whenever new artifacts are produced. 

**Deploy to dev & production environments**

Manually trigger a CI build to push the code to dev & production environment. 

**Create a change request work item**

Create a work item corresponding to a fictional change request for the *Clients* web application. 

## Task# 1: Set up your Visual Studio Team Services account

**NOTE:** If you are running Linux on your local workstation, install git:

 `sudo apt-get install git`
 
**NOTE:** If you are running Windows, you have to install the git client from here:

[http://git-scm.com/download](https://git-scm.com/download/win)

We want to push the PartsUnlimitedMRP application code to your Visual Studio Team Services account in order to use Team Build for the various demos.

0. First, navigate to your Visual Studio Team Services account by typing into a browser: 

 `https://{VSTS instance name}.visualstudio.com`

0. Once at your Visual Studio Team Services account, create a new *PartsUnlimitedMRP* team project by clicking on the **New** button under **Recent projects & teams**. Type in the project name as *PartsUnlimitedMRP* and select *Git* as the version control, then click on **Create project**.

 ![](<media/create_team_project.png>)

0. After the wizard creates your new team project, navigate to the *PartsUnlimitedMRP* team project and click on the **Code** tab on the upper-left. 

 ![](<media/navigate_to_code.png>)

0. The *PartsUnlimitedMRP* Git repository will be empty, so copy the Clone URL of the VSTS repository to your clipboard and paste it into a text document for use later. 

 ![](<media/copy_vsts_repo_url.png>)

0. Clone the **PartsUnlimitedMRP** git repository onto your local machine, open your command-line tool (perhaps in your "Documents" folder) and type :

 `git clone <CloneURL> PartsUnlimitedMRP`

 ![](<media/clone_mrp.png>)

0. Change directory

 `cd PartsUnlimitedMRP`

0. Copy the file *PartsUnlimitedMRP.zip* (found under the Resources folder with these presentation materials) into this folder, open it, select all the files/folders in it, then copy the contents into this PartsUnlimitedMRP folder.

 ![](<media/extracted_files_in_reposirory_folder.png>)

0. Back at the command line, add/stage all files with git, commit them and push to VSTS

 ```
 git add . --force

 git commit -m "Initial commit"

 git push origin
 ```
 
 [This step can take several minutes (1-3 minutes) depending on your connection speed.]
 ![](<media/push_to_vsts.png>)

 **NOTE:** If it's the first time that you are using your Visual Studio Team Services subscription, it will ask you to be authenticated.

 ![](<media/vsts_auth.png>)


0. If you refresh your **CODE** section page, Your Visual Studio Team Services account should now have a copy of the PartsUnlimitedMRP
application:

 ![](<media/mrp_in_vsts.png>)

## Task# 2: Provision the Dev & Prod environment ###

> Instead of manually creating the VMs in Azure, we are going to use a PowerShell script & an Azure Resource Management (ARM) template. 

[Make sure you have Azure PowerShell installed from the machine running the script (see [prerequisites](#prerequisites)).]

0. In a PowerShell window, log in to Microsoft Azure
                                                                    
 `Login-AzureRmAccount`
 
0. Launch the deployment

 ```
 .\docs\Sessions\SETUP_Continuous-Deployment\env\setup_env.ps1 -SubscriptionName <Your_Subscription_Name> -DnsPrefixNameForPublicIP <UniqueDnsName>
 ```

 > Where UniqueDnsName should be a name you pick that is globally unique in Azure and will be used in the name of provisioned Azure resources (e.g. MyUPMRP20161001).
 > The VMs will be deployed to Resource Groups along with a virtual network (VNET) and some other required resources. 
 > You can delete the resource groups in order to remove all the created resources at any time.

0. When the deployment completes, you should see the following resources in the Azure Portal:

 ![](<media/post_deployment_rg.png>)

 > Take note of the information in the output generated by the script as you will need it later, especially the URL.

 ![](<media/setup_env_script_output.png>)

 > The solution requires several ports to be open, such as SSH ports and the Parts Unlimited MRP app port on the partsmrp machine. 
 > The ARM template opens these ports on the machine for you.

## Task# 3: Create a Continuous Integration Build

0. Once on the project’s homepage, click on the **Build** hub at the top of the page.

 ![](<media/build_tab.png>)

0. Click the **+ New Definition** button (or the **green “plus” sign** or the **+ New** link), select **Empty**, and then click **Next >**.

 ![](<media/new_empty_build.png>)

0. Ensure the Team Project is selected as the **Repository source**, the appropriate repository (created in the previous step), and select "Hosted" as the **Default agent queue**, then click **Create**.

 ![](<media/build_select_repo.png>)

0. Click on the **Build** tab, click **Add build step...**, and then click the **Add** button three times next to the Gradle task to add three Gradle tasks to the script. Gradle will be used to build the Integration Service, Order Service, and Clients components of the MRP app.

 ![](<media/build_add_gradle.png>)

0. Select the first Gradle task and **click the pencil icon** to edit the task name. Name the task *IntegrationService* and set the **Gradle Wrapper** to the following location in the VSTS code repository you published (either type or browse using the **...** button):

 `src/Backend/IntegrationService/gradlew`

 ![](<media/build_gradle_integration.png>)

0. Uncheck the checkbox in **JUnit Test Results** to Publish to TFS/Team Services since we will not be running automated tests in the Integration Service. Expand the **Advanced** section, and set the **Working Directory** to the following location:

 `src/Backend/IntegrationService`

 ![](<media/build_working_directory_integration.png>)

0. Select the second Gradle task and **click the pencil icon** to edit the task name. Name the task *OrderService* and set the **Gradle Wrapper** to the following location:

 `src/Backend/OrderService/gradlew`

 ![](<media/build_gradle_order.png>)
 
0. Since Order Service does have unit tests in the project, we can automate running the tests as part of the build by adding in `test` in the **Gradle tasks** field. Keep the **JUnit Test Results** checkbox to "Publish to TFS/Team Services" checked, and set the **Test Results Files** field to `**/TEST-*.xml`. 

0. Expand the **Advanced** section, and set the **Working Directory** to the following location:

 `src/Backend/OrderService`

 ![](<media/build_working_directory_order.png>)

0. Select the third Gradle task and **click the pencil icon** to edit the task name. Name the task *Clients* and set the **Gradle Wrapper** to the following location:

 `src/Clients/gradlew`

 ![](<media/build_gradle_clients.png>)

0. Uncheck the checkbox in **JUnit Test Results** to Publish to TFS/Team Services since we will not be running automated tests in Clients. Expand the **Advanced** section, and set the **Working Directory** to the following location:

 `src/Clients`

 ![](<media/build_working_directory_clients.png>)

0. Click **Add build step...**, select the **Utility** tab, and add two **Copy and Publish Build Artifacts** tasks.

 ![](<media/build_add_pub_step.png>)

0. Select the first Publish Build Artifacts task, and fill in the input values with the following:

 ```
 Copy Root: $(Build.SourcesDirectory)
 Contents: **/build/libs/!(buildSrc)*.?ar
 Artifact Name: drop
 Artifact Type: Server
 ```
 ![](<media/build_pub_step_details.png>)

0. Select the second Publish Build Artifacts task, and fill in the input values with the following:

 ```
 Copy Root: $(Build.SourcesDirectory)
 Contents: **/deploy/*.*
 Artifact Name: deploy
 Artifact Type: Server
 ```
 
 ![](<media/second_copy_publish.png>)

0. Go to the **Triggers** tab and **select Continuous Integration (CI)**.

 ![](<media/build_ci_trigger.png>)

0. Click **General**, set the default queue to the appropriate queue. Leave as the **Hosted** queue from the previous steps.

 ![](<media/build_general.png>)

0. Click **Save**, give the build definition a name (strongly suggesting *PartsUnlimitedMRPCI*), and then click **OK**.

 ![](<media/build_save.png>)


### Task# 4: Create release definition ###

0. At the homepage of the PartsUnlimitedMRP team project in Visual Studio Team Services, click on the **Release** tab in the upper-left corner of the page. Then, click the **New definition** button on the homepage.

 ![](<media/new_release.png>)

0. In the **Create new release definition** dialog, choose an empty template then the OK button. 

 ![](<media/create_empty_definition.png>)

0. Keep the artifacts as **Build**, select the CI build definition that you used in the previous step (such as "PartsUnlimitedCI"), check the checkbox to enable the **Continuous Deployment trigger**, and choose "Hosted" as the  **agent queue**.

 ![](<media/choose_source_queue_new_dialog.png>) 

0. Click on the **Environment** keyword and rename the environment to be **"Dev"**.
0. Click on the pencil icon on the top of the definition and rename it to be `PartsUnlimitedMRPCD`. 

 ![](<media/change_environment_name.png>)

0. Click twice (2x) the **Add tasks** button and add a PowerShell script task (under the Utility category). 

 ![](<media/add_powershell_script.png>)

0. For the first PowerShell task, name it `Configure APM`, and fill in the input values with the following:

 ```
 Type:           File Path
 ScriptPath:     $(System.DefaultWorkingDirectory)/PartsUnlimitedMRPCI/deploy/Set-APMInDrop.ps1
 Arguments:      -InstrumentationKey $(instrumentationKey)
 ```

0. For the second PowerShell task, name it `Deploy MRP using sftp and SSH through PowerShell`, and fill in the input values with the following:
 ```
 Type:           File Path
 ScriptPath:     $(System.DefaultWorkingDirectory)/PartsUnlimitedMRPCI/deploy/SSH-MRP-Artifacts.ps1
 Arguments:      -sshTarget '$(sshTarget)' -sshUser '$(sshUser)' -sshPassword '$(sshPassword)' 
 ```
 
0. In the environment box, click on the ellipses ("...") and select **Configure variables...** option. 

 ![](<media/configure_variables.png>)

0. Create four variables: `sshUser`, `sshPassword`, `sshTarget` & `instrumentationKey`. Fill in the values of the virtual machine that you created previously using the ARM template. `sshTarget` should be the public DNS name of the virtual machine, such as "mylinuxvm.westus.cloudapp.azure.com."

 ![](<media/fill_in_variable_values.png>)

0. Click on **Advanced** to expand the panel and untick the option : **Fail on Standard Error** to avoid some garbage warning.

 ![](<media/ssh_errors.png>)    

0. Click on the **Triggers** tab and set the artifact source by selecting the Build definition that you are created previously.

 ![](<media/vsts_CD.png>)
 
0. Save the release definition. 

#### Create a "Prod" environment ###

0. Right click the **Dev** environment ellipsis (...) and click **Clone environment**.

 ![](<media/clone_dev_environment.png>)

0. Choose *Specific users* and enter your user in the text box. Ensure the **Deploy automatically whenever a deployment to Dev is successful** is checked.

 ![](<media/add_prod_environment.png>)

0. Rename the new environment to **Prod**.

 ![](<media/rename_prod_environment.png>)

0. Click the **Prod** environment ellipsis and click *Configure Variables...* and change the sshTarget for the right production VM hostname.

0. Ensure you fill again the `sshUser`, `sshPassword`, `sshTarget` & the `instrumentationKey` with production environment infos.

 ![](<media/fill_in_variable_values-prod.png>)

0. Save the Release Definition


### Task# 5: Deploy to dev & production environments ###

0. At the homepage of the PartsUnlimitedMRP team project in Visual Studio Team Services, click on the **Build** tab in the upper-left corner of the page. 

0. Click the **PartsUnlimitedMRPCI** ellipsis button, then click **Queue new build**.

 ![](<media/manual_queue_ci_build.png>)

0. Click the **OK** button in the queue build dialog.

 ![](<media/queue_new_ci_build_dialog.png>)

0. Wait for the CI build to complete.

0. Click the **Release** tab in the upper-left corner of the page, wait for the first environment to be green

0. Click the **clock** icon, then click **(Override)**

 ![](<media/browse_to_cd_prod_override.png>)

0. Click the **Approve** button.

 ![](<media/override_approve_cd_build_to_prod.png>)

### Task# 6: Create a change request work item ###

0. At the homepage of the PartsUnlimitedMRP team project in Visual Studio Team Services, click on the **Work** tab in the upper-left corner of the page. Then, click the **Title** textbox and enter *Change Clients web site title to 'Parts Unlimited MRP - Beta'*. Then click the **Add** button.

 ![](<media/create_change_title_workitem.png>)

0. Right-click the work item that you have just created and choose **Assign to >** menu item, then choose yourself.

 ![](<media/assign_change_request.png>)