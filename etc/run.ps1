Invoke-Expression "./etc/setup-infra.ps1"

# Navigate to the PlayTicket.AppHost and run the migration
Set-Location -Path "./PlayTicket.AppHost"
try {
    dotnet run
} catch {
    Write-Error "Failed to run the .NET Aspire"
    exit 1
}


