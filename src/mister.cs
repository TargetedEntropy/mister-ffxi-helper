using System;
using System.Diagnostics;
using System.Collections.Generic;
using EliteMMO.API;

namespace Mister
{
    public class FFXI
    {
        public static List<Process> matchingProcesses = new List<Process>();
        private static string CharacterName;

        private static EliteAPI POL_Instance;

        static FFXI()
        {
            matchingProcesses = GetProcessesByName("pol");
            CharacterName = SelectCharacter();
            POL_Instance = CreateInstance(CharacterName);
        }
        public List<Process> ReturnProcessList()
        {
            return matchingProcesses;
        }

        private static EliteAPI CreateInstance(string CharacterName)
        {
            int pol_process = matchingProcesses.First(a => a.MainWindowTitle == CharacterName).Id;
            return new EliteAPI(pol_process);
        }

        private static string SelectCharacter()
        {
            Console.WriteLine("Select your Character:");
            foreach (Process process in matchingProcesses)
            {
                Console.WriteLine($"    {process.MainWindowTitle}");
            }
            Console.WriteLine("Name:");
            string response = Console.ReadLine();
            if (response == "")
                Environment.Exit(0);
            return response;
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
