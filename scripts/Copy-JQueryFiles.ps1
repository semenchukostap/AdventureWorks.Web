#############################################################################
# jQuery Library Files Copy Script (PowerShell)
# Purpose: Copy jQuery v3.3.1 library files from legacy app to new .NET 8 app
# Usage: .\scripts\Copy-JQueryFiles.ps1
#############################################################################

# Set error action preference
$ErrorActionPreference = "Stop"

# Banner
Write-Host "========================================" -ForegroundColor Green
Write-Host "jQuery v3.3.1 Migration Script" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

# Get script directory and repository root
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RepoRoot = Split-Path -Parent $ScriptDir

# Source and destination paths
$SourceDir = Join-Path $RepoRoot "wwwroot\lib\jquery\dist"
$DestDir = Join-Path $RepoRoot "new_app\wwwroot\lib\jquery\dist"

Write-Host "Source:      " -NoNewline -ForegroundColor Yellow
Write-Host $SourceDir
Write-Host "Destination: " -NoNewline -ForegroundColor Yellow
Write-Host $DestDir
Write-Host ""

# Check if source directory exists
if (-not (Test-Path $SourceDir)) {
    Write-Host "Error: Source directory does not exist: $SourceDir" -ForegroundColor Red
    exit 1
}

# Create destination directory
Write-Host "Creating destination directory..." -ForegroundColor Yellow
New-Item -ItemType Directory -Path $DestDir -Force | Out-Null

# Files to copy
$Files = @(
    "jquery.js",
    "jquery.min.js",
    "jquery.min.map"
)

# Copy each file
Write-Host "Copying jQuery library files..." -ForegroundColor Yellow
Write-Host ""

$CopySuccess = $true

foreach ($file in $Files) {
    $SourceFile = Join-Path $SourceDir $file
    $DestFile = Join-Path $DestDir $file
    
    if (Test-Path $SourceFile) {
        Write-Host "  ➜ Copying " -NoNewline
        Write-Host $file -NoNewline -ForegroundColor Green
        Write-Host "..."
        
        Copy-Item -Path $SourceFile -Destination $DestFile -Force
        
        # Verify copy
        if (Test-Path $DestFile) {
            $SourceSize = (Get-Item $SourceFile).Length
            $DestSize = (Get-Item $DestFile).Length
            
            if ($SourceSize -eq $DestSize) {
                Write-Host "    ✓ Success (" -NoNewline
                Write-Host "$SourceSize bytes" -NoNewline -ForegroundColor Green
                Write-Host ")"
            }
            else {
                Write-Host "    ✗ Size mismatch!" -ForegroundColor Red
                $CopySuccess = $false
                break
            }
        }
        else {
            Write-Host "    ✗ Copy failed!" -ForegroundColor Red
            $CopySuccess = $false
            break
        }
    }
    else {
        Write-Host "  ✗ Source file not found: $file" -ForegroundColor Red
        $CopySuccess = $false
        break
    }
}

if (-not $CopySuccess) {
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "Verification" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

# Verify jQuery version
Write-Host "Verifying jQuery version..." -ForegroundColor Yellow
$JQueryFile = Join-Path $DestDir "jquery.js"
$FirstLines = Get-Content $JQueryFile -First 15 | Out-String
$VersionMatch = [regex]::Match($FirstLines, "v(\d+\.\d+\.\d+)")

if ($VersionMatch.Success -and $VersionMatch.Groups[1].Value -eq "3.3.1") {
    Write-Host "  ✓ jQuery version: " -NoNewline
    Write-Host "v$($VersionMatch.Groups[1].Value)" -ForegroundColor Green
}
else {
    Write-Host "  ✗ Version mismatch: Expected v3.3.1" -ForegroundColor Red
    exit 1
}

# List copied files
Write-Host ""
Write-Host "Copied files:" -ForegroundColor Yellow
Get-ChildItem $DestDir | ForEach-Object {
    $SizeKB = [math]::Round($_.Length / 1KB, 2)
    Write-Host "  $($_.Name) - $SizeKB KB"
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "✓ jQuery Migration Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

# Git status
Write-Host "Git Status:" -ForegroundColor Yellow
Write-Host "To commit these changes, run:"
Write-Host "  git add new_app/wwwroot/lib/jquery/dist/" -ForegroundColor Green
Write-Host "  git commit -m 'Copy jQuery v3.3.1 library files to new_app'" -ForegroundColor Green
Write-Host ""

exit 0
