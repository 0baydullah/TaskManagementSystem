@echo off
set Report_Folder_Name=UnitTestResult
set Report_File_Name=coverage
set DotNet_Path=dotnet

:: Step 1: Build the solution
echo "Building the solution..."
%DotNet_Path% build ProjectManagementTool.sln

:: Step 2: Create the report folder if it does not exist
echo "Creating a report folder if it does not exist..."
IF exist "%Report_Folder_Name%" (
    echo "%Report_Folder_Name% exists"
) ELSE (
    mkdir "%Report_Folder_Name%"
    echo "%Report_Folder_Name% created"
)

:: Step 3: Run unit tests with coverage
echo "Running unit tests with coverage..."
%DotNet_Path% test --no-build --collect:"XPlat Code Coverage" --results-directory:".\%Report_Folder_Name%" BusinessLogicLayer.Test

:: Step 4: Install ReportGenerator if not already installed
echo "Installing ReportGenerator if not already installed..."
%DotNet_Path% tool install -g dotnet-reportgenerator-globaltool 2>nul || echo "ReportGenerator already installed"

:: Step 5: Generate Code Coverage Report from XML
echo "Generating Code Coverage Report from XML"
:: Use the full path to the reportgenerator tool
"%USERPROFILE%\.dotnet\tools\reportgenerator" -reports:".\%Report_Folder_Name%\**\coverage.cobertura.xml" -targetdir:".\%Report_Folder_Name%" -reporttypes:HTML;HTMLSummary

:: Step 6: Open results in the default browser
echo "Opening results in default browser"
start "" ".\%Report_Folder_Name%\index.html"