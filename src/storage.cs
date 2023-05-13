using EliteMMO.API;
using System;
using System.Text.Json;
using System.Text;

namespace Mister
{
    public class Storage
    {
        public async Task Run(EliteAPI api)
        {
            int[] containers = GetContainers(api);
            await GetContainerItems(api, containers);
        }

        private static int[] GetContainers(EliteAPI api)
        {
            int[] used_containers = new int[0];
            for (int i = 0; i <= 16; i++)
            {
                int inv_count = api.Inventory.GetContainerCount(i);
                if (inv_count > 0)
                {
                    Array.Resize(ref used_containers, used_containers.Length + 1);
                    used_containers[used_containers.Length - 1] = i;
                }
            }
            return used_containers;
        }

        private async Task GetContainerItems(EliteAPI api, int[] containers)
        {
            using (HttpClient httpClient = new HttpClient())

                foreach (int container in containers)
                {
                    int item_count = api.Inventory.GetContainerCount(container);
                    for (int i = 1; i <= item_count; i++)
                    {
                        EliteAPI.InventoryItem item = api.Inventory.GetContainerItem(container, i);
                        Console.WriteLine($"Container: {(StorageContainer)container} ItemId: {item.Id} Amount: {item.Count}");
                        string character_name = api.Player.Name;
                        string url = "";

                        await postItemData(url, character_name, container, item.Id, item.Count);

                    }
                }

        }

        private static async Task postItemData(string url, string character_name, int container_id, int item_id, uint item_count)
        {
            using var httpClient = new HttpClient();

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    character_name = character_name,
                    container_id = container_id,
                    item_id = item_id,
                    item_count = item_count
                }),
                Encoding.UTF8,
                "application/json");
            await httpClient.PostAsync(url, jsonContent).ConfigureAwait(false);

        }

    }
}