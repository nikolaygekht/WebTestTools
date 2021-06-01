@echo off
dotnet build ../webviewtest.sln /p:Configuration=Release
dotnet build project.proj /t:Scan,Raw