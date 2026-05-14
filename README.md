# ShroundZoneHelper

This is a C# WinForms application for managing, tracking and viewing board game "Shround Zone War" data.

## Project structure

The workspace contains two language variants of the same WinForms project:

- `en_US/` — English version
- `zh_CN/` — Chinese version

Each folder contains a Visual Studio solution and project for the WinForms app.

## Requirements

- DOTNET supported OS
- .NET 10 SDK
- Visual Studio Code

Alternatively, you can use the .NET CLI to build and run the project.

## Run with Visual Studio Code

0. Install DOTNET on Microsoft website(dotnet.microsoft.com). Add the installation path to system environment variable "PATH". Install required extensions in vscode (C#, C# Dev Kit).
1. Open either folder `en_US\` or `zh_CN\` in Visual Studio Code.
2. Set the startup project if necessary.
3. Press **F5** or click **Run->Start Degugging** to build and run the application.

## Run with .NET CLI
Install DOTNET on Microsoft website(dotnet.microsoft.com) if you don't have one.

Open a PowerShell or Command Prompt and navigate to the desired language folder, for example, in a random Windows computer:

```powershell
D:

cd "D:\Users\UserName\Download\ShroundZoneWarHelper\en_US"
```

Then run:

```powershell
dotnet build
dotnet run
```

The application should launch as a Windows Forms window.

## Notes for non-C# users

- `dotnet build` compiles the application.
- `dotnet run` builds and starts the application in one step.
- If you are not familiar with C#, using Visual Studio Code is the easiest option because it provides a graphical interface for opening, building, and running the WinForms app.

## Files to launch as a solution

- `en_US\ShroundZoneHelper.sln`
- `zh_CN\ShroundZoneHelper.sln`

If you want the English UI, open the `en_US` solution. For the Chinese UI, open the `zh_CN` solution.
