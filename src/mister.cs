using System.Diagnostics;
using EliteMMO.API;

namespace Mister
{
    public class FFXI
    {
        public static List<Process> matchingProcesses = new List<Process>();
        private static string CharacterName;
        public static EliteAPI POL_Instance;
        static Dictionary<uint, DateTime> grayList = new Dictionary<uint, DateTime>();
        static string pyxis = "";

        static FFXI()
        {
            matchingProcesses = GetProcessesByName("pol");
            CharacterName = SelectCharacter();
            POL_Instance = CreateInstance(CharacterName);
        }
        public EliteAPI GetFFXIInstance()
        {
            return POL_Instance;
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
            Console.Write("Name: ");
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

        public bool CheckItem(EliteAPI api, string name)
        {
            for (int i = 0; i < api.Inventory.GetContainerMaxCount(0); i++)
            {
                if (api.Inventory.GetContainerItem(0, i).Id != 0 && api.Resources.GetItem(api.Inventory.GetContainerItem(0, i).Id).Name.First() == name) return true;
            }
            return false;
        }

        public static EliteAPI.XiEntity Box(EliteAPI api)
        {
            TargetInfo TargetInfo = new TargetInfo(api);
            for (var x = 0; x < 2048; x++)
            {
                EliteAPI.XiEntity entity = api.Entity.GetEntity(x);
                if (entity.WarpPointer == 0 || entity.HealthPercent == 0 || entity.TargetID <= 0)
                    continue;
                if (entity.Distance < 10 && entity.Name == "Sturdy Pyxis") pyxis = entity.Face + " " + entity.Render0000;
                if (entity.Distance < 10 && entity.Name == "Sturdy Pyxis" && (!grayList.ContainsKey(entity.TargetID) || grayList[entity.TargetID] < DateTime.Now))
                {
                    if (entity.Face == 965) return entity;
                    if (entity.Face == 968) return entity;
                }
            }
            return null;
        }

        public void OpenBoxes()
        {
            EliteAPI api = GetFFXIInstance();
            if (api == null) return;
            
            PlayerInfo PlayerInfo = new PlayerInfo(api);
            TargetInfo TargetInfo = new TargetInfo(api);
            Movement Movement = new Movement(api);
            Recast Recast = new Recast(api);

            System.Threading.Thread.Sleep(100);

            if (api.Player.Status == 0)
            {
                if (Box(api) != null && CheckItem(api, "Forbidden Key"))
                {
                    if (TargetInfo.ID != Box(api).TargetID)
                    {
                        TargetInfo.SetTarget((int)Box(api).TargetID);
                        return;
                    }
                    else if (TargetInfo.Distance > 3)
                    {
                        Movement.Move(Box(api));
                        return;
                    }
                    else
                    {
                        Movement.Stop();
                        api.ThirdParty.SendString("/item \"Forbidden Key\" <t>");
                        if (!grayList.ContainsKey(Box(api).TargetID))
                            grayList.Add(Box(api).TargetID, DateTime.Now.AddSeconds(20));
                        else
                            grayList[Box(api).TargetID] = DateTime.Now.AddSeconds(20);
                        Thread.Sleep(1500);
                        return;
                    }
                }
            }

        }


    }

}
