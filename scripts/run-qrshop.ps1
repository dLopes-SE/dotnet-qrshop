# =====================================================
# 🚀 QRShop Remote Launcher (Always Pull Latest)
# =====================================================

# -----------------------------------------------------
# Parameters (optional)
# -----------------------------------------------------
param(
    [string]$StripeSecretKey = "",
    [string]$StripePublishableKey = ""
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
# 💳 Ask for optional Stripe backend secret if not provided
# -----------------------------------------------------
if (-not $StripeSecretKey) {
    $StripeSecretKey = Read-Host "💳 Enter Stripe Secret Key (backend, optional, press Enter to skip)"
}

# 💳 Ask for optional Next.js Stripe publishable key if not provided
if (-not $StripePublishableKey) {
    $StripePublishableKey = Read-Host "💳 Enter Stripe Publishable Key (frontend, optional, press Enter to skip)"
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

# Set environment variables for Docker Compose
$env:STRIPE_SECRET_KEY = $StripeSecretKey
$env:NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY = $StripePublishableKey

try {
    # Pull latest images
    docker compose -f $tmpComposeFile pull

    # Start containers
    docker compose -f $tmpComposeFile up -d
} catch {
    Write-Error "❌ Docker Compose failed. QRShop is NOT running."
    Remove-Item $tmpComposeFile
    exit 1
}

# -----------------------------------------------------
# 🧹 Cleanup
# -----------------------------------------------------
Remove-Item $tmpComposeFile

# -----------------------------------------------------
# ✅ Success
# -----------------------------------------------------
Write-Host ""
Write-Host "✅ QRShop is up and running!"
Write-Host "👉 Frontend: http://localhost:3000"
Write-Host "👉 Backend API: http://localhost:5000"
Write-Host ""
