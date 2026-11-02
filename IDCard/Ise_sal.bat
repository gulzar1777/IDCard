@echo off
setlocal
cd /d "%~dp0"
set "IDCARD_CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
if not exist "%IDCARD_CSC%" set "IDCARD_CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe"
if not exist "%IDCARD_CSC%" (
    echo .NET Framework 4.8 is required. Or open IDCard.sln in Visual Studio.
    pause
    exit /b 1
)
if not exist "bin\Release" mkdir "bin\Release"
"%IDCARD_CSC%" /nologo /target:winexe /platform:anycpu /codepage:65001 /out:"bin\Release\IDCard.exe" /reference:System.dll /reference:System.Core.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll Program.cs Form1.cs Form1.Designer.cs PreviewControls.cs Properties\AssemblyInfo.cs
if errorlevel 1 (
    echo Build failed. Open IDCard.sln in Visual Studio to see details.
    pause
    exit /b 1
)
copy /y "App.config" "bin\Release\IDCard.exe.config" >nul
start "" "bin\Release\IDCard.exe"
endlocal
