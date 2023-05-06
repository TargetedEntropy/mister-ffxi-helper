using System.Diagnostics;
using EliteMMO.API;

namespace Mister
{
    public class Mandy
    {   
        public Mandy(EliteAPI api) 
        {
            EnterMandyHell(api);
            PlayMandyHell(api);
        }

        public static void PlayMandyHell(EliteAPI api) {
                string dialogtext = api.Dialog.GetDialogText();

                while (api.Dialog.DialogOptionCount == 0)
                {
                    System.Threading.Thread.Sleep(500);
                } 

                int count = 0;
                string[] dialog_split_period = dialogtext.Split(".");
                foreach (string dialog in dialog_split_period) {
                    if (count == 0) {
                        count++;
                        continue;
                    }
                    if (dialog.Contains("mandra")) {
                        char mandy_count_text = dialog[2];
                        if (mandy_count_text.ToString() != "0") {
                                api.ThirdParty.KeyPress(Keys.RETURN);
                                System.Threading.Thread.Sleep(1500);
                        } else {
                                api.ThirdParty.KeyPress(Keys.DOWN);
                                System.Threading.Thread.Sleep(1500);
                        }
                        Console.WriteLine($"mandy_count_text:{mandy_count_text}");

                        count++;
                    }
                }
        }

        public static bool EnterMandyHell(EliteAPI api) {
            
            if (api.Player.ZoneId != 280) {
                Console.WriteLine("Unable to enter Mandy Hell, you must be in the Mog Garden");
                return false;
            }

            // We are not Idle, we cannot enter
            if (api.Player.Status != 0) return false;

            Console.WriteLine("Setting Target");
            // Set the Mandy as target
            bool tabresult = SetTargetByTabbing(api, 17924237);
            if (tabresult) {
                // Open Mandy Dialog
                api.ThirdParty.KeyPress(Keys.RETURN);
                System.Threading.Thread.Sleep(1500);
            }

            if (api.Dialog.DialogIndex == 0) {
                // Play Random
                api.ThirdParty.KeyPress(Keys.RETURN);
                System.Threading.Thread.Sleep(2500);
            }
            return true;
            
        } 
        private static bool SetTargetByTabbing(EliteAPI api, int target)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (target != api.Target.GetTargetInfo().TargetId)
            {
                if (stopwatch.Elapsed >= TimeSpan.FromSeconds(1))
                {
                    break;
                }

                api.ThirdParty.KeyPress(Keys.TAB);
                TimeWaiter.Pause(200);
            } 
            if (target == api.Target.GetTargetInfo().TargetId) {                
                return true;
            } else
                return false;
        }


    }

    public class TimeWaiter
    {
        public static bool IsEnabled { get; set; } = true;

        public static void Pause(int milliseconds)
        {
            if (IsEnabled)
            {
                Thread.Sleep(milliseconds);
            }
        }
    }

 
}