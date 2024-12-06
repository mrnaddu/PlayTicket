# Run the setup script
try {
    Invoke-Expression "./etc/setup-infra.ps1"
} catch {
    Write-Error "Failed to execute setup-infra.ps1"
    exit 1
}

# Navigate to the PlayTicket.DbMigrator directory and run the migration
Set-Location -Path "./shared/PlayTicket.DbMigrator"
try {
    dotnet run
} catch {
    Write-Error "Failed to run the .NET migration"
    exit 1
}

# Return to the original directory
Set-Location -Path "../.."
