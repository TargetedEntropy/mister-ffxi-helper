using EliteMMO.API;
using System.Threading;

namespace Mister
{
    class Program
    {
        static void Main(string[] args)
        {
            FFXI misterFF = new FFXI();

            Thread MainThread = new Thread(() => MainThreadFunc(misterFF));
            MainThread.Start();

            Thread StatusThread = new Thread(() => StatusThreadFunc(misterFF));
            StatusThread.Start();

        }
        
        static void MainThreadFunc(Mister.FFXI misterFF) {
            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                System.Threading.Thread.Sleep(500);

                misterFF.OpenBoxes();
                Console.WriteLine("tick");
            }            
        }

        static void StatusThreadFunc(Mister.FFXI misterFF) {
            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                System.Threading.Thread.Sleep(500);

                Console.WriteLine("tock");

            }    
        }

   }
}
