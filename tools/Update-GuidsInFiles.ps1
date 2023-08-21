#Requires -Version 7

Param(
    [Parameter(Mandatory = $true, Position = 1)]
    [alias("p")][string]
    $Path,

    [Parameter(Mandatory = $true, Position = 2)]
    [alias("f")][string]
    $Filter
)
# setup error handling policy
$errorActionPreference = "Stop"

# check executable path
$exePath = "$PSScriptRoot/../bin/Release/net6.0/ReplaceWithGuid.exe"
if (-not (Test-Path $exePath)) {
    $exePath ="$PSScriptRoot/../bin/Debug/net6.0/ReplaceWithGuid.exe"
    if (-not (Test-Path $exePath)) {
        Write-Error "ReplaceWithGuid.exe not found."
    }
}
Write-Host "Using executable: $exePath"

# update files
$files = Get-ChildItem -Path $Path -Filter $Filter -Recurse
$files | ForEach-Object {
    &$exePath $_
}
