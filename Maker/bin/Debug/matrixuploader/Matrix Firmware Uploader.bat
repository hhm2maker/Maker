@echo off
cd /d "%~dp0"

echo Matrix Firmware Uploader V1.00
echo.

dfu-util -v -d 0203:0100,0203:0003 -t 2048 -a 0 -R -D %1
pause >nul