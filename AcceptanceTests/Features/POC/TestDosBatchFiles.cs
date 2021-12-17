using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTests.Common.Library;
using System.Diagnostics;

namespace AcceptanceTests.Features.POC
{
    class TestDosBatchFiles
    {

        [Test]
        //Run DOS commands from C# code
        public void RunDosCmdinCode()
        {

            //processInfo = new ProcessStartInfo("cmd.exe", "/c " + @"dir c:\");
            ProcessStartInfo  processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processInfo.FileName = "cmd.exe";
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.Arguments = @"/C dir c:\";              //Use this to pass arguments
            //processInfo.WorkingDirectory = @"C:\";
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            Process process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            int ExitCode = process.ExitCode;

            //MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");

            process.Close();

        }

            [Test]
        /// <summary>
        /// Execute DOS Batch script file using C# code.
        /// The code just executes the Batch script, has no idea
        /// what the script does
        /// </summary>
        public void RunDosBatchScript()
        {
            //Get the project dir
            var runDir = Library.GetProjectDir();
            var filePath = runDir + @"\" + "AcceptanceTests" + @"\" + "DOSBatchFiles";
            string fileName = "DosBatchListDirs.bat";
            string path = filePath + @"\" + fileName;

            int ExitCode;
            ProcessStartInfo processInfo;
            Process process;

            //processInfo = new ProcessStartInfo(path,"arg1 arg2 arg3"); //Start batch file WITH arguments
            processInfo = new ProcessStartInfo(path);                 //Start batch file WITH NO arguments
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = filePath;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            ExitCode = process.ExitCode;

            //MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
           
            process.Close();


        }

      

    }
}
