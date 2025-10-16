#!/usr/bin/env bash
set -e

# =====================================================
# 🚀 QRShop Remote Launcher (Always Pull Latest)
# =====================================================

COMPOSE_URL="https://raw.githubusercontent.com/dlopes-se/dotnet-qrshop/main/docker-compose.demo.yml"

echo ""
echo "🧩 QRShop setup starting..."
echo ""

# -----------------------------------------------------
# 🧠 Check dependencies
# -----------------------------------------------------
check_command() {
  local cmd=$1
  local name=$2
  if ! command -v "$cmd" >/dev/null 2>&1; then
    echo "❌ Error: $name is not installed."
    echo "👉 Please install it first and try again."
    exit 1
  fi
}

check_command docker "Docker"

# Docker Compose v2 is part of Docker CLI
if ! docker compose version >/dev/null 2>&1; then
  echo "❌ Error: Docker Compose v2 is not available."
  echo "👉 Please ensure your Docker version supports 'docker compose'."
  exit 1
fi

echo "✅ Docker and Compose detected."
echo ""

# -----------------------------------------------------
# 💳 Ask for optional Stripe key
# -----------------------------------------------------
echo "💳 You can optionally provide your Stripe Secret Key."
read -p "Enter Stripe Secret Key (or press Enter to skip): " STRIPE_SECRET_KEY

# -----------------------------------------------------
# 🧱 Download docker-compose.yml
# -----------------------------------------------------
TMP_COMPOSE_FILE=$(mktemp /tmp/docker-compose.XXXXXX.yml)
echo "⬇️  Downloading docker-compose.yml from GitHub..."
curl -fsSL "$COMPOSE_URL" -o "$TMP_COMPOSE_FILE"

# -----------------------------------------------------
# 🖥️ Detect OS and run
# -----------------------------------------------------
OS="$(uname | tr '[:upper:]' '[:lower:]')"

echo ""
echo "🐳 Pulling latest images and starting containers..."
echo ""

if [[ "$OS" == "windows_nt" || "$OS" == *"mingw"* ]]; then
  echo "Detected Windows PowerShell environment..."
  pwsh -Command "
    \$env:STRIPE_SECRET_KEY='$STRIPE_SECRET_KEY';
    \$nullEnv = New-TemporaryFile;
    docker compose --env-file \$nullEnv -f $TMP_COMPOSE_FILE pull;
    docker compose --env-file \$nullEnv -f $TMP_COMPOSE_FILE up -d;
    Remove-Item \$nullEnv
  "
else
  STRIPE_SECRET_KEY="$STRIPE_SECRET_KEY" docker compose --env-file /dev/null -f "$TMP_COMPOSE_FILE" pull
  STRIPE_SECRET_KEY="$STRIPE_SECRET_KEY" docker compose --env-file /dev/null -f "$TMP_COMPOSE_FILE" up -d
fi

# -----------------------------------------------------
# 🧹 Cleanup
# -----------------------------------------------------
rm -f "$TMP_COMPOSE_FILE"

# -----------------------------------------------------
# ✅ Done
# -----------------------------------------------------
echo ""
echo "✅ QRShop is up and running!"
echo "👉 Frontend: http://localhost:3000"
echo "👉 Backend API: http://localhost:5000"
echo ""
