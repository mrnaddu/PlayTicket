$ErrorActionPreference = "Stop"  # Stop execution on any error

# Function to invoke a script and check for errors
function Invoke-Script {
    param (
        [string]$scriptPath
    )
    
    if (Test-Path $scriptPath) {
        Write-Host "`nInvoking script: $scriptPath..." -ForegroundColor Cyan
        try {
            Invoke-Expression $scriptPath
            Write-Host "Script $scriptPath executed successfully." -ForegroundColor Green
        } catch {
            Write-Host "Error executing script $scriptPath: $_" -ForegroundColor Red
            exit 1
        }
    } else {
        Write-Host "`nError: Script path $scriptPath does not exist." -ForegroundColor Red
        exit 1
    }
}

# Change directory to the setup script path and execute it
Invoke-Script "./etc/setup-infra.ps1"

# Change location to the shared DbMigrator directory
Write-Host "`nChanging directory to './shared/PlayTicket.DbMigrator'..." -ForegroundColor Cyan
Set-Location "./shared/PlayTicket.DbMigrator"

# Run the dotnet application
Write-Host "`nRunning DbMigrator..." -ForegroundColor Cyan
try {
    dotnet run
    Write-Host "DbMigrator executed successfully." -ForegroundColor Green
} catch {
    Write-Host "Error executing DbMigrator: $_" -ForegroundColor Red
    exit 1
}

# Change location back to the original directory
Write-Host "`nReturning to the original directory..." -ForegroundColor Cyan
Set-Location "../.."

Write-Host "`nProcess completed successfully." -ForegroundColor Cyan
