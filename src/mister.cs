using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Mister
{
    public class FFXI
    {
        public static List<Process> matchingProcesses = new List<Process>();

        static FFXI()
        {
            matchingProcesses = GetProcessesByName("pol");
        }
        public List<Process> ReturnProcessList()
        {
            return matchingProcesses;
        }

        private static List<Process> GetProcessesByName(string processName)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.Contains(processName))
                    {
                        matchingProcesses.Add(process);
                    }
                }

                return matchingProcesses;
        }    

    }

}
