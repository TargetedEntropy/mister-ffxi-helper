using EliteMMO.API;

namespace Mister
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Replace("--", "");
            }
            if (args.Contains("help"))
            {
                Console.WriteLine("Options:");
                Console.WriteLine("   --boxes : Open Abyssea Chests");
                Console.WriteLine("   --status : Give Character status updates");
                Environment.Exit(0);
            }

            FFXI misterFF = new FFXI();

            if (args.Contains("boxes"))
            {
                Thread MainThread = new Thread(() => MainThreadFunc(misterFF));
                MainThread.Start();
            }

            if (args.Contains("status"))
            {
                Thread StatusThread = new Thread(() => StatusThreadFunc(misterFF));
                StatusThread.Start();
            }            

        }

        static void MainThreadFunc(Mister.FFXI misterFF)
        {
            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                System.Threading.Thread.Sleep(500);

                misterFF.OpenBoxes();
            }
        }

        static void StatusThreadFunc(Mister.FFXI misterFF)
        {
            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                Console.Clear();

                Console.WriteLine((EliteMMO.API.EntityStatus)api.Player.Status);

                System.Threading.Thread.Sleep(100);
                
            }
        }

    }
}
