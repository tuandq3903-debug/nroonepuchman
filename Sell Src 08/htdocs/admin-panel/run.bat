@echo off
title NRO Admin Panel
color 0A

echo ================================================
echo    NRO Admin Panel - Khoi Dong Server
echo ================================================
echo.

cd /d "%~dp0"

echo Dang kiem tra Node.js...
node --version >nul 2>&1
if errorlevel 1 (
    echo [LOI] Node.js chua duoc cai dat!
    echo Vui long cai dat Node.js tai: https://nodejs.org/
    pause
    exit /b 1
)

echo Node.js da duoc cai dat: 
node --version
echo.

echo Dang kiem tra dependencies...
if not exist "node_modules" (
    echo Dang cai dat dependencies...
    call npm install
    if errorlevel 1 (
        echo [LOI] Khong the cai dat dependencies!
        pause
        exit /b 1
    )
)

echo.
echo ================================================
echo    Khoi dong Admin Panel...
echo ================================================
echo.
echo Server se chay tai: http://localhost:3000
echo Dashboard: http://localhost:3000/dashboard.html
echo.
echo Nhan Ctrl+C de dung server
echo ================================================
echo.

npm start

pause
