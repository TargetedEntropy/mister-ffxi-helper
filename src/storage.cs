using EliteMMO.API;

namespace Mister
{
    public class Storage
    {
        public Storage(EliteAPI api)
        {
            GetContainers(api);
        }

        public static int[] GetContainers(EliteAPI api)
        {
            int[] used_containers = new int[0];
            for (int i = 0; i <= 16; i++)
            {
                int inv_count = api.Inventory.GetContainerCount(i);
                if (inv_count > 0) {
                    Array.Resize(ref used_containers, used_containers.Length + 1);
                    used_containers[used_containers.Length - 1] = i;
                }
            }
            return used_containers;
        }
    }
}