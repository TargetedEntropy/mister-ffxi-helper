using EliteMMO.API;
using System.Threading;

namespace Mister
{
    class Program
    {
        static Thread MainThread = new Thread(() => MainThreadFunc());

        static void Main(string[] args)
        {
            
            MainThread.Start();
        }
        
        static void MainThreadFunc() {

            FFXI misterFF = new FFXI();

            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                System.Threading.Thread.Sleep(500);

                misterFF.OpenBoxes();
            }            
        }

   }
}
