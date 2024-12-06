# Log function to standardize log output with timestamps
function Write-Log {
    param (
        [string]$Message,
        [string]$Level = "INFO"
    )

    $timestamp = (Get-Date).ToString("yyyy-MM-dd HH:mm:ss")
    Write-Host "[$timestamp] [$Level] $Message"
}

# Validate the action argument
$action = $args[0]
$subaction = $args[1]

if (-not $action) {
    Write-Log "`nWelcome to Play ticket..`" -Level "INFO"
    Write-Log "`nPlease specify an action to perform.`" -Level "ERROR"
    exit 1
}

# Normalize the action to lowercase for easier comparison
$action = $action.ToLower()

# Define a helper function to invoke scripts with error handling
function Invoke-Script {
    param (
        [string]$scriptPath
    )

    if (Test-Path $scriptPath) {
        Write-Log "Executing script: $scriptPath" -Level "INFO"
        try {
            Invoke-Expression $scriptPath
            Write-Log "Successfully executed script: $scriptPath" -Level "INFO"
        } catch {
            Write-Log "Error executing script '$scriptPath': $_" -Level "ERROR"
            exit 2
        }
    } else {
        Write-Log "Error: Script '$scriptPath' not found." -Level "ERROR"
        exit 3
    }
}

# Main action processing
switch ($action) {
    "install" {
        Write-Log "Starting installation process..." -Level "INFO"
        Invoke-Script "./etc/setup.ps1"
    }

    "infra" {
        switch ($subaction) {
            "up" {
                Write-Log "Starting infrastructure (up) process..." -Level "INFO"
                Invoke-Script "./etc/docker/up.ps1"
            }

            "down" {
                Write-Log "Starting infrastructure (down) process..." -Level "INFO"
                Invoke-Script "./etc/docker/down.ps1"
            }

            default {
                Write-Log "`nInvalid subaction for 'infra'. Use 'up' or 'down'." -Level "ERROR"
                exit 4
            }
        }
    }

    "configure-cs" {
        Write-Log "Starting CS configuration process..." -Level "INFO"
        Invoke-Script "./etc/configure-cs.ps1"
    }

    "run" {
        Write-Log "Starting run process..." -Level "INFO"
        Invoke-Script "./etc/run.ps1"
    }

    "migrate" {
        Write-Log "Starting migration process..." -Level "INFO"
        Invoke-Script "./etc/migrate.ps1"
    }

    default {
        Write-Log "`nError: Unknown action '$action'. Please specify a valid action." -Level "ERROR"
        exit 5
    }
}

Write-Log "Script execution completed." -Level "INFO"
