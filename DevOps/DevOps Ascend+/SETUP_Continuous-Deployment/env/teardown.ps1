#requires -version 4.0
param(
    [Parameter(Mandatory=$true)]
    [string]
    $SubscriptionName 
)
#Log into Azure if a context is not already present
$ErrorActionPreference = 'SilentlyContinue'
Login-AzureRmAccount
$ErrorActionPreference = 'Stop'

Select-AzureRmSubscription -SubscriptionName $SubscriptionName

Write-Host 'Removing Dev environment'
Remove-AzureRmResourceGroup -Name 'PartsUnlimitedMRP-dev'

Write-Host 'Removing Prod environment'
Remove-AzureRmResourceGroup -Name 'PartsUnlimitedMRP-prod'