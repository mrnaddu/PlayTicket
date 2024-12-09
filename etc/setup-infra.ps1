$ErrorActionPreference = "Stop"  # Stop execution on any error

# Define required services to check
$requiredServices = @(
    'mysql-db',
    'redis',
    'seq',
    'rabbit-mq',
)

# Function to check if a service is running in Docker
function Check-DockerServiceStatus {
    param (
        [string]$serviceName
    )

    # Filter Docker ps output based on the service name
    $serviceRunningStatus = docker ps --filter "name=$serviceName" 2>&1

    if ($serviceRunningStatus -match "Error") {
        Write-Host "`nError while checking the status of $serviceName. Docker may not be running." -ForegroundColor Red
        return $false
    }

    # Check if the service is running
    $isServiceUp = $serviceRunningStatus -split " " -contains $serviceName
    return $isServiceUp
}

# Iterate over each required service and check if it's running
foreach ($requiredService in $requiredServices) {
    Write-Host "`nChecking service: $requiredService..." -ForegroundColor Cyan

    $isRunning = Check-DockerServiceStatus -serviceName $requiredService

    if ($isRunning) {
        Write-Host "$requiredService is [up]" -ForegroundColor Green
    } else {
        Write-Host "$requiredService is [down]. Attempting to start Docker containers..." -ForegroundColor Yellow

        # Start the required services if any are down
        try {
            Invoke-Expression "./etc/docker/up.ps1"
            Write-Host "Attempted to start Docker containers." -ForegroundColor Green
        } catch {
            Write-Host "Failed to start Docker containers. Please check the configuration." -ForegroundColor Red
            exit 1
        }
    }
}

Write-Host "`nService check complete." -ForegroundColor Cyan
