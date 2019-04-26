@echo off
cd /d "%~dp0"

echo Matrix Firmware Uploader V1.00
echo.

if "%~f1" == "" (
echo No file detected, please drag^&drop .mxfw file on to the exe file. Press any key to exit. 
pause >nul
exit
)

if NOT "%~x1" == ".mxfw" (
echo This isn't a Matrix Firmware file ^(.mxfw^). Press any key to exit. 
pause >nul
exit
)


echo Make sure Matrix is pluged in. Press Any Key to continue.
pause >nul
echo.

dfu-util -v -d 0203:0100,0203:0003 -t 2048 -a 0 -R -D "%~f1"
pause >nul