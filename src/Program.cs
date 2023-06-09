﻿using EliteMMO.API;

namespace Mister
{
    class Program
    {
        static async Task Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Replace("--", "");
            }
            if (args.Contains("help") || args.Length == 0)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("   --boxes : Open Abyssea Chests");
                Console.WriteLine("   --status : Give Character status updates");
                Console.WriteLine("   --mandy : Play the Mandy Game - WorkInProgress");
                Console.WriteLine("   --storage : Dump Storage");

                Environment.Exit(0);
            }

            FFXI misterFF = new FFXI();
            EliteAPI api = misterFF.GetFFXIInstance();

            if (args.Contains("boxes"))
            {
                Thread BoxThread = new Thread(() => BoxThreadFunc(misterFF));
                BoxThread.Start();
            }

            if (args.Contains("status"))
            {
                Thread StatusThread = new Thread(() => StatusThreadFunc(api));
                StatusThread.Start();
            }

            if (args.Contains("storage"))
            {
                Storage storage = new Storage();
                await storage.Run(api);
            }

            if (args.Contains("mandy"))
            {
                Mandy mandy = new Mandy(misterFF.GetFFXIInstance());
            }

        }

        static void BoxThreadFunc(Mister.FFXI misterFF)
        {
            // Breathe first
            System.Threading.Thread.Sleep(500);

            while (1 == 1)
            {
                EliteAPI api = misterFF.GetFFXIInstance();
                if (api == null) continue;

                misterFF.OpenBoxes();
                System.Threading.Thread.Sleep(500);
            }
        }

        static void StatusThreadFunc(EliteAPI api)
        {
            while (1 == 1)
            {

                if (api == null) continue;

                Console.Clear();

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                Console.WriteLine($"Status: {(EliteMMO.API.EntityStatus)api.Player.Status}");
                Console.WriteLine($"TargetID: {api.Target.GetTargetInfo().TargetId}");
                Console.WriteLine($"Zone: {(Zone)api.Player.ZoneId}");
                Console.WriteLine("----------------");


                System.Threading.Thread.Sleep(100);


            }
        }

    }
}
