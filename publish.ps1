$ErrorActionPreference = "Stop"

Write-Host ">> Portfolio publish..." -ForegroundColor Cyan
dotnet publish Portfolio_ZoranSimeunovic.csproj -c Release -o publish --nologo
if (-not $?) { Write-Host "GRESKA: Portfolio publish nije uspio." -ForegroundColor Red; exit 1 }

Write-Host ">> SyncWorkerService publish..." -ForegroundColor Cyan
dotnet publish SyncWorkerService\SyncWorkerService.csproj -c Release -o publish-sync --nologo
if (-not $?) { Write-Host "GRESKA: SyncWorkerService publish nije uspio." -ForegroundColor Red; exit 1 }

Write-Host ">> Gotovo." -ForegroundColor Green
