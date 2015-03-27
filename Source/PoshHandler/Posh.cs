using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Collections;

namespace PoshHandler
{
    class Posh : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request; // Store the context of the request which includes the requested file info
            HttpResponse response = context.Response; // Store the reponse being built to send back
            DateTime dt; // Just used to store time and show back in header or other artifact.
            dt = DateTime.UtcNow;
            
            response.Write("<html>");
            response.Write("<body>");
            response.Write(
                string.Format("<h1>Hello, this is a PowerShell HTTP handler.{0}</h1>",
                dt.ToLongTimeString()
                ));
            response.Write(
                string.Format("<div>{0}</div>",
                RunScript(request.PhysicalPath) // Runs the function that creates a PowerShell runspace and runs the script
                // # TODO: Fix this method to be able to pass a hashtable representing the context as a parameter to the script
                //RunScriptWParameters(request.PhysicalPath,context)
                ));
            response.Write("</body>");
            response.Write("</html>");
        }
        /// <summary>
        ///  Method used to run script that was in URL, or default file as part of IIS settings.
        /// </summary>
        /// <param name="scriptPath"> This is the script path derived from the HTTP Request Context </param>
        /// <returns></returns>
        private string RunScript(string scriptPath)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace(); 
            runspace.ApartmentState = System.Threading.ApartmentState.STA;
            runspace.ThreadOptions = PSThreadOptions.UseCurrentThread;
            
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptPath);
            // # This is a sample adding modules. 
            // # TODO: Parse through special diretory in app folders and add each file as module.
            //pipeline.Commands.Add("Import-Module");
            //var command = pipeline.Commands[0];
            //command.Parameters.Add("Name", @"G:\PowerShell\PowerDbg.psm1") 
            pipeline.Commands.Add("Out-String");

            Collection<PSObject> results = pipeline.Invoke();

            runspace.Close();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///  This method will eventually run the script with the context as a hashtable parameter passed in.
        ///  No time to complete now, will invest time later.
        /// </summary>
        /// <param name="scriptPath">File path of the script to run.</param>
        /// <param name="context">HTTP Request Context</param>
        /// <returns></returns>
        private string RunScriptWParameters(string scriptPath, HttpContext context)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                ArrayList contextlist = new ArrayList();
                string script = File.ReadAllText(scriptPath);
                PowerShellInstance.AddScript(script);
                PowerShellInstance.AddParameter("context", contextlist);
                Collection<PSObject> results = PowerShellInstance.Invoke();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject obj in results)
                {
                    stringBuilder.AppendLine(obj.Properties["DisplayName"].Value.ToString());
                    //stringBuilder.AppendLine(obj.ToString());
                }

                return stringBuilder.ToString();
            }
        }
        
    }
}
