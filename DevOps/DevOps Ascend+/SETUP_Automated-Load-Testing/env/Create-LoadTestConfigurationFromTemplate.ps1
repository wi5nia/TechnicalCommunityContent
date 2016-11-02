Param(
    [Parameter(Mandatory=$True)]
    [string]$WorkingDirectory,

    [Parameter(Mandatory=$True)]
    [string]$CloudAppName
)

(Get-Content $WorkingDirectory\MRP_load_test_template.jmx).replace('[[CLOUDAPP_NAME]]', $CloudAppName) | Set-Content $WorkingDirectory\MRP_load_test.jmx