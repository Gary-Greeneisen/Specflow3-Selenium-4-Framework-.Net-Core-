@@echo off
rem filename - RunTestsVSTest.cmd
rem Batch file using VSTest to execute 
rem How to Run .NET Core Selenium Tests in Azure DevOps
rem for help enter dotnet vstest /help

Set "ProjectDir=C:\Test1\Specflow3-Selenium-4-Framework-.Net-Core\bin\Debug\net5.0\"
Set "ProjectName=AcceptanceTests.dll"
Set "TestProject=%ProjectDir%%ProjectName%"

echo "ProjectDir ProjectName = "
echo Starting Test(s) in %TestProject%
rem pause

rem Run NUnit test drivers test
dotnet vstest %TestProject% /Tests:TestBrowserDrivers /logger:"trx;logfilename=TestResults1.xml"
rem pause

rem Run Specflow tags test
dotnet vstest %TestProject%  /TestCaseFilter:"TestCategory=GoogleSearchFeature" /logger:"trx;logfilename=TestResults2.xml"

rem Run Both NUnit tests and Specflow tags test
rem dotnet vstest %TestProject%  /Tests:TestBrowserDrivers,GoogleSearchFeature /logger:"trx;logfilename=TestResults.xml"
rem pause









