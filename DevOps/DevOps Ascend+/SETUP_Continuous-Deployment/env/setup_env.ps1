#requires -version 4.0
param(
    [Parameter(Mandatory=$true)]
    [string]
    $SubscriptionName,    
    [Parameter(Mandatory=$true)]
    [string]
    $DnsPrefixNameForPublicIP,
    [Parameter()]
    [string]
    $AdminUsername = 'azureuser',
    [Parameter()]
    [string]
    $AdminPassword = '8iC73psK6eX/9cTXmpw!MfEeO',
    [Parameter()]
    [string]
    $Location = 'West US'
)
#Log into Azure if a context is not already present
$ErrorActionPreference = 'SilentlyContinue'
Login-AzureRmAccount
$ErrorActionPreference = 'Stop'

Select-AzureRmSubscription -SubscriptionName $SubscriptionName

$currentDir = Split-Path $script:MyInvocation.MyCommand.Path
$templatePath = Join-Path $currentDir ContinuousDeploymentPartsUnlimitedMRP.json

Write-Host 'Creating Dev environment'
$devRg = New-AzureRmResourceGroup -Name 'PartsUnlimitedMRP-dev' -Location $Location -Force
$devEnvParams = @{
    mrpAdminUsername = $AdminUsername
    mrpAdminPassword = $AdminPassword
    mrpDnsNameForPublicIP = '{0}-dev' -f $DnsPrefixNameForPublicIP
}
$devDeploy = New-AzureRmResourceGroupDeployment -Name 'dev-env' -ResourceGroupName $devRg.ResourceGroupName -TemplateFile $templatePath -TemplateParameterObject $devEnvParams -Verbose

Write-Host 'Creating Prod environment'
$prodRg = New-AzureRmResourceGroup -Name 'PartsUnlimitedMRP-prod' -Location $Location -Force
$prodEnvParams = @{
    mrpAdminUsername = $AdminUsername
    mrpAdminPassword = $AdminPassword
    mrpDnsNameForPublicIP = '{0}-prod' -f $DnsPrefixNameForPublicIP
}
$prodDeploy = New-AzureRmResourceGroupDeployment -Name 'prod-env' -ResourceGroupName $prodRg.ResourceGroupName -TemplateFile $templatePath -TemplateParameterObject $prodEnvParams -Verbose

Write-Host 'Getting provisionned resources information...'
$devFqdn = (Get-AzureRmPublicIpAddress -ResourceGroupName $devRg.ResourceGroupName).DnsSettings.fqdn
$prodFqdn = (Get-AzureRmPublicIpAddress -ResourceGroupName $prodRg.ResourceGroupName).DnsSettings.fqdn

$devEnvParams.Add('hostname', $devFqdn)
$devEnvParams.Add('MRP url', ('http://{0}:9080/mrp' -f $devFqdn))
$devEnvParams.Add('AppInsights Intrumentation Key', $devDeploy.Outputs.appInsightsInstrumentationKey.value)
Write-Host
Write-Host 'Dev environment details'
$devEnvParams | Format-Table -Force

$prodEnvParams.Add('hostname', $prodFqdn)
$prodEnvParams.Add('MRP url', ('http://{0}:9080/mrp' -f $prodFqdn))
$prodEnvParams.Add('AppInsights Intrumentation Key', $prodDeploy.Outputs.appInsightsInstrumentationKey.value)
Write-Host 'Prod environment details'
$prodEnvParams | Format-Table -Force