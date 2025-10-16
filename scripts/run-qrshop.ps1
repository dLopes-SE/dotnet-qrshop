# -----------------------------------------------------
# 🚀 QRShop Remote Launcher (Always Pull Latest)
# -----------------------------------------------------

# -----------------------------------------------------
# Parameters
# -----------------------------------------------------
param(
    [string]$StripeSecretKey = ""
)

# -----------------------------------------------------
# Constants
# -----------------------------------------------------
$COMPOSE_URL = "https://raw.githubusercontent.com/dlopes-se/dotnet-qrshop/main/docker-compose.demo.yml"

Write-Host ""
Write-Host "🧩 QRShop setup starting..."
Write-Host ""

# -----------------------------------------------------
# 🧠 Check dependencies
# -----------------------------------------------------
function Check-Command {
    param([string]$Cmd, [string]$Name)
    if (-not (Get-Command $Cmd -ErrorAction SilentlyContinue)) {
        Write-Error "❌ Error: $Name is not installed."
        Write-Host "👉 Please install it first and try again."
        exit 1
    }
}

Check-Command -Cmd "docker" -Name "Docker"

# Docker Compose v2 is part of Docker CLI
try {
    docker compose version | Out-Null
} catch {
    Write-Error "❌ Error: Docker Compose v2 is not available."
    Write-Host "👉 Please ensure your Docker version supports 'docker compose'."
    exit 1
}

Write-Host "✅ Docker and Compose detected."
Write-Host ""

# -----------------------------------------------------
# 💳 Ask for optional Stripe key if not provided
# -----------------------------------------------------
if (-not $StripeSecretKey) {
    $StripeSecretKey = Read-Host "💳 Enter Stripe Secret Key (or press Enter to skip)"
}

# -----------------------------------------------------
# 🧱 Download docker-compose.yml
# -----------------------------------------------------
$tmpComposeFile = [System.IO.Path]::GetTempFileName()
Invoke-WebRequest -Uri $COMPOSE_URL -OutFile $tmpComposeFile

# -----------------------------------------------------
# 🐳 Pull and start containers
# -----------------------------------------------------
Write-Host ""
Write-Host "🐳 Pulling latest images and starting containers..."
Write-Host ""

# Set environment variable for Docker Compose
$env:STRIPE_SECRET_KEY = $StripeSecretKey

# Use a "null" env file so Docker Compose doesn’t complain
if ($IsWindows) {
    $nullEnv = [System.IO.Path]::GetTempFileName()
    docker compose --env-file $nullEnv -f $tmpComposeFile pull
    docker compose --env-file $nullEnv -f $tmpComposeFile up -d
    Remove-Item $nullEnv
} else {
    docker compose --env-file /dev/null -f $tmpComposeFile pull
    docker compose --env-file /dev/null -f $tmpComposeFile up -d
}

# -----------------------------------------------------
# 🧹 Cleanup
# -----------------------------------------------------
Remove-Item $tmpComposeFile

# -----------------------------------------------------
# ✅ Done
# -----------------------------------------------------
Write-Host ""
Write-Host "✅ QRShop is up and running!"
Write-Host "👉 Frontend: http://localhost:3000"
Write-Host "👉 Backend API: http://localhost:5000"
Write-Host ""
