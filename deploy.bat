@ECHO OFF
powershell -NoProfile -ExecutionPolicy Bypass -Command "& '.\build.ps1'" -Target DeployToS3
PAUSE