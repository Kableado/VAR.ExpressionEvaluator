@echo off

:: MSBuild and tools path
if exist "%windir%\Microsoft.Net\Framework\v4.0.30319" set MsBuildPath=%windir%\Microsoft.NET\Framework\v4.0.30319
if exist "%windir%\Microsoft.Net\Framework64\v4.0.30319" set MsBuildPath=%windir%\Microsoft.NET\Framework64\v4.0.30319
if exist "C:\Program Files (x86)\MSBuild\14.0\Bin" set MsBuildPath=C:\Program Files (x86)\MSBuild\14.0\Bin
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin" set MsBuildPath=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin" set MsBuildPath=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin
set PATH=%MsBuildPath%;%PATH%

echo %MsBuildPath%

:: NuGet
set nuget="nuget"
if exist "%~dp0\..\packages\NuGet.CommandLine.5.3.1\tools\NuGet.exe" set nuget="%~dp0\..\packages\NuGet.CommandLine.5.3.1\tools\NuGet.exe"

:: Release
Title Building Release
msbuild VAR.ExpressionEvaluator.csproj /t:Build /p:Configuration="Release" /p:Platform="AnyCPU"

:: Packing Nuget
Title Packing Nuget
%nuget% pack VAR.ExpressionEvaluator.csproj -Verbosity detailed -OutputDir "NuGet" -Properties Configuration="Release" -Prop Platform=AnyCPU

title Finished
pause
