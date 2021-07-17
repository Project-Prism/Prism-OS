@echo off
title Prism QEMU

if not "%minimized%"=="" goto :minimized
set minimized=true
start /min cmd /C "Prism.cmd"
goto :EOF
:minimized

set ram=1024M
set ISOpath=%USERPROFILE%\source\repos\Prism-OS\PrismProject\bin\Debug\netcoreapp2.0\cosmos\PrismProject.iso
"C:\Program Files\qemu\qemu-system-i386.exe" -m %ram% -cdrom %ISOpath%