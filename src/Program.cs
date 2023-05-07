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
                Console.WriteLine("   --mandy : Play the Mandy Game - WorkInProgress");
                Console.WriteLine("   --storage : Dump Storage");

                Environment.Exit(0);
            }

            FFXI misterFF = new FFXI();

            if (args.Contains("boxes"))
            {
                Thread BoxThread = new Thread(() => BoxThreadFunc(misterFF));
                BoxThread.Start();
            }

            if (args.Contains("status"))
            {
                Thread StatusThread = new Thread(() => StatusThreadFunc(misterFF));
                StatusThread.Start();
            }

            if (args.Contains("storage"))
            {
                Thread StorageThread = new Thread(() => StorageThreadFunc(misterFF));
                StorageThread.Start();
            }

            if (args.Contains("mandy"))
            {
                Mandy mandy = new Mandy(misterFF.GetFFXIInstance());
            }                        

        }

        static void BoxThreadFunc(Mister.FFXI misterFF)
        {
            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                System.Threading.Thread.Sleep(500);

                misterFF.OpenBoxes();
            }
        }

        static void StorageThreadFunc(Mister.FFXI misterFF)
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

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                Console.WriteLine($"Status: {(EliteMMO.API.EntityStatus)api.Player.Status}");
                Console.WriteLine($"TargetID: {api.Target.GetTargetInfo().TargetId}");
                Console.WriteLine($"ZoneID: {api.Player.ZoneId}");
                Console.WriteLine("----------------");


                System.Threading.Thread.Sleep(100);

                
            }
        }

    }
}
