
using System.Diagnostics;
using EliteMMO.API;

namespace Mister
{
    class Program
    {
        static void Main(string[] args)
        {
            FFXI myClass = new FFXI();
            List<Process> processes = myClass.ReturnProcessList();

            Console.WriteLine("Select your Character:");
            int count = 0;
            foreach (Process process in processes)
            {
                Console.WriteLine($"{count}: {process.MainWindowTitle}");
                count++;
            }
            Console.WriteLine("Name:");
            string response = Console.ReadLine();
            if (response == "") 
                Environment.Exit(0);

            int pol_process = processes.First(a => a.MainWindowTitle == response).Id;
            Console.WriteLine(pol_process);
            EliteAPI TheInstance  = new EliteAPI(pol_process);
            Console.WriteLine(TheInstance.Player.GetPlayerInfo().Homepoint);

        }
    }


}
