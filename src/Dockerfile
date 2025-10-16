# ==============================
# Build stage
# ==============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Publish in Release mode to a clean output folder
RUN dotnet publish -c Release -o /app/publish --no-restore

# ==============================
# Runtime stage
# ==============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose port (optional, for clarity)
EXPOSE 5000

# ASP.NET will listen on this URL
ENV ASPNETCORE_URLS=http://+:5000

# Start the app
ENTRYPOINT ["dotnet", "dotnet-qrshop.dll"]
