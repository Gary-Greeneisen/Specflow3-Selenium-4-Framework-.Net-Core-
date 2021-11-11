@@echo off
rem filename - RunTestsDotNetTest.cmd
rem Batch file using dotnet test to execute 
rem How to Run .NET Core Selenium Tests in Azure DevOps
rem for help enter dotnet test --help

Set "ProjectDir=C:\Test1\Specflow3-Selenium-4-Framework-.Net-Core\bin\Debug\net5.0\"
Set "ProjectName=AcceptanceTests.dll"
Set "TestProject=%ProjectDir%%ProjectName%"

echo "ProjectDir ProjectName = "
echo Starting Test(s) in %TestProject%
rem pause

rem Run NUnit test drivers test
dotnet test %TestProject% --filter TestBrowserDrivers --logger "trx;logfilename=TestResults1.xml"

rem Run Specflow tags test
dotnet test %TestProject% --filter "TestCategory=GoogleSearchFeature" --logger "trx;logfilename=TestResults2.xml"

rem Run NUnit tests and Specflow tags tests
rem dotnet test %TestProject% --filter "TestBrowserDrivers | TestCategory=GoogleSearchFeature"  --logger "trx;logfilename=TestResults.xml"
rem pause











