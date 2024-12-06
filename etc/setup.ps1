$ErrorActionPreference = "Stop"  # Stop execution on any error

# Function to check if a command exists
function Check-CommandExistence {
    param (
        [string]$commandName
    )
    return [bool] (Get-Command -ErrorAction Ignore -Type Application $commandName)
}

# Verify if Docker is installed
$dockerExist = Check-CommandExistence -commandName "docker"
if (-not $dockerExist) {
    Write-Host "`nDocker is not installed. Please install Docker to proceed." -ForegroundColor Red
    exit
}

# Verify Docker is running
$dockerStatus = docker ps 2>&1
if ($dockerStatus -match "Error") {
    Write-Host "`nDocker is not running. Please start Docker before proceeding." -ForegroundColor Red
    exit
}

# Install Dotnet EF tool
Write-Host "`nInstalling Dotnet EF Tool..." -ForegroundColor Green
dotnet tool install --global dotnet-ef --version 8.0.2

# Install ABP Studio CLI
Write-Host "`nInstalling ABP Studio CLI..." -ForegroundColor Green
dotnet tool install -g Volo.Abp.Studio.Cli

# Install Tye globally
Write-Host "`nInstalling Tye..." -ForegroundColor Green
dotnet tool install -g Microsoft.Tye --version "0.11.0-alpha.22111.1"

# Build the solution
Write-Host "`nBuilding solution..." -ForegroundColor Green
dotnet build /graphBuild

# Start Infrastructure and Apply Migrations
Write-Host "`nStarting infrastructure setup and applying migrations..." -ForegroundColor Green
Invoke-Expression "./playticket.ps1 infra up"
Invoke-Expression "./playticket.ps1 migrate"

# Completion message
Write-Host "`nProject setup is complete!" -ForegroundColor Cyan
Write-Host "You can now run the project using: '.\playticket.ps1 run'" -ForegroundColor Cyan
