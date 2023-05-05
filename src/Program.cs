using EliteMMO.API;

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
            EliteAPI api = misterFF.GetFFXIInstance();
            while (1 == 1)
            {
                if (api == null) continue;

                misterFF.OpenBoxes(api);
            }            
        }
   }
}
