using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Diagnostics;
using AcceptanceTests.Common.Library;

namespace AcceptanceTests.Features.POC
{
    class TestPowerShell
    {

         [Test]
         //Run PowerShell commands from C# code
        public void RunPowerShellinCode()
        {
            /********************************************
                PowerShell.Create().AddCommand("Echo")
                     .AddParameter("This is a Test")
                    .Invoke();
            ********************************************/

            //Create PowerShell command
            PowerShell psinstance = PowerShell.Create();
            psinstance.AddCommand("Echo");
            psinstance.AddParameter("This is a Test");
            var results = psinstance.Invoke();

            //Display results in Visual Studio Tests Output Window
            System.Diagnostics.Debug.Write(results.ToString());
            //TestContext.Progress.WriteLine(results.ToString());

            //Execute same PowerShell command using differnt output
            // invoke execution on the pipeline (collecting output)
            Collection<PSObject> PSOutput = psinstance.Invoke();

            //Display results in Visual Studio Tests Output Window
            // loop through each output object item
            foreach (PSObject outputItem in PSOutput)
            {
                // if null object was dumped to the pipeline during the script then a null object may be present here
                if (outputItem != null)
                {
                    Console.WriteLine($"Output line: [{outputItem}]");
                }
            }

        }


        [Test]

        //invoke PowerShell process from C# code and pass command as argument.
        //See below example how execute PowerShell process from code and pass cmdlet as argument.
        public void RunPowerShellWindow()
        {
            //execute powershell cmdlets or scripts using command arguments as process
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = @"powershell.exe";
            //execute powershell script using script file
            //processInfo.Arguments = @"& {c:\temp\Get-EventLog.ps1}";
            //execute powershell command
            processInfo.Arguments = @"echo 'This is a Test'";
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;

            //start powershell process using process start info
            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();

            //read output
            Console.WriteLine("Output - {0}", process.StandardOutput.ReadToEnd());
            //read errors
            Console.WriteLine("Errors - {0}", process.StandardError.ReadToEnd());
            Console.Read();

        }

        [Test]
        public void RunCmdWindow()
        {
            //Open cmd window (DOS prompt)
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = @"C:\WINDOWS\system32\cmd.exe";
            processInfo.WindowStyle = ProcessWindowStyle.Normal;

            //start CMD process using process start info
            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();

            //Display a message
            Console.WriteLine("This is CMD message, Press any key");
            Console.ReadKey();

        }

        [Test]
        /// <summary>
        /// Execute PowerShell script file using C# code.
        /// The code just executes the PowerShell script, has no idea
        /// what the script does
        /// </summary>
        public void RunPowerShellScript()
        {
            //Get the project dir
            var runDir = Library.GetProjectDir();
            var filePath = runDir +  @"\"  + "AcceptanceTests" + @"\" + "PowerShell";
            //string fileName = "PowerShellReadFile.ps1";
            string fileName = "PowerShellListDirs.ps1";
            
            string path = filePath + @"\" + fileName;

            PowerShell powerShell = PowerShell.Create();
            powerShell.AddScript(System.IO.File.ReadAllText(path));
            Collection<PSObject> PSOutput = powerShell.Invoke();

        }






        }
}
