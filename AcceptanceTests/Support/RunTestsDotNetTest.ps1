#filename - RunTestsDotNetTest.ps1
#Power Shell Batch file using VSTest to execute tests
#for help at the Power Shell prompt type Get-Help
#Get-Help

$ProjectDir = "C:\Test1\Specflow3-Selenium-4-Framework-.Net-Core\bin\Debug\net5.0\"
$ProjectName = "AcceptanceTests.dll"
$TestProject = $ProjectDir + $ProjectName

Write-Host "ProjectDir ProjectName = "
Write-Host "Starting Test(s) in " $TestProject

#pause the script
#read-host “Press ENTER to continue...”

#Run NUnit test drivers test
dotnet test $TestProject --filter TestBrowserDrivers --logger "trx;logfilename=TestResults1.xml"
#pause the script
#read-host “Press ENTER to continue...”

#Run Specflow tags test
dotnet test $TestProject --filter:"TestCategory=GoogleSearchFeature" --logger "trx;logfilename=TestResults2.xml"

#Run NUnit tests and Specflow tags tests
#dotnet test $TestProject --filter "TestBrowserDrivers | TestCategory=GoogleSearchFeature"  --logger "trx;logfilename=TestResults.xml"
#read-host “Press ENTER to continue...”









