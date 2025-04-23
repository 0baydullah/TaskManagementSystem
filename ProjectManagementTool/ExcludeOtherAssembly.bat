@echo off
set Report_Folder_Name=UnitTestResult
set Report_File_Name=coverage
set DotNet_Path=dotnet

:: Step 1: Build the solution
echo "Building the solution..."
%DotNet_Path% build ProjectManagementTool.sln

:: Step 2: Delete and recreate the report folder
echo "Checking for existing report folder..."
IF exist "%Report_Folder_Name%" (
    echo "Existing report folder found. Deleting..."
    rmdir /s /q "%Report_Folder_Name%"
)

echo "Creating a new report folder..."
mkdir "%Report_Folder_Name%"

:: Step 3: Run unit tests with coverage for BusinessLogicLayer only
echo "Running unit tests with coverage for BusinessLogicLayer..."
%DotNet_Path% test BusinessLogicLayer.Test --no-build --collect:"XPlat Code Coverage" --results-directory:".\%Report_Folder_Name%" --filter "FullyQualifiedName~BusinessLogicLayer"

:: Step 4: Install ReportGenerator if not already installed
echo "Installing ReportGenerator if not already installed..."
%DotNet_Path% tool install -g dotnet-reportgenerator-globaltool 2>nul || echo "ReportGenerator already installed"

:: Step 5: Generate Code Coverage Report for BusinessLogicLayer only
echo "Generating Code Coverage Report..."
"%USERPROFILE%\.dotnet\tools\reportgenerator" ^
 -reports:".\%Report_Folder_Name%\**\coverage.cobertura.xml" ^
 -targetdir:".\%Report_Folder_Name%" ^
 -reporttypes:Html;HtmlSummary ^
 -assemblyfilters:"+BusinessLogicLayer;-DataAccessLayer;-ProjectManagementTool"

:: Step 6: Open results in the default browser
echo "Checking if report file exists..."
if exist ".\%Report_Folder_Name%\index.html" (
    echo "Opening results in default browser..."
    start "" ".\%Report_Folder_Name%\index.html"
) else (
    echo "Error: No coverage report generated. Please check the logs."
)
