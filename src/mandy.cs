using System.Diagnostics;
using EliteMMO.API;

namespace Mister
{
    public class Mandy
    {   
        public Mandy(EliteAPI api) 
        {
            EnterMandyHell(api);
        }
        public static bool EnterMandyHell(EliteAPI api) {
            
            // We are not Idle, we cannot enter
            if (api.Player.Status != 0) return false;

            Console.WriteLine("Setting Target");
            SetTargetByTabbing(api, 17924237);
            
            return true;
            
        } 
        private static void SetTargetByTabbing(EliteAPI api, int target)
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