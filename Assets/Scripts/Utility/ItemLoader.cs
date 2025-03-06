using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor.PackageManager;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class ItemLoader
{
    private CancellationTokenSource _cancellationTokenSource;
    private readonly GameServerMock gameServerMock = new GameServerMock();

    public async Task<ItemData> FetchItems()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        ItemData itemData = null;
        try
        {
            string jsonResult = await gameServerMock.GetItemsAsync(_cancellationTokenSource.Token);
            itemData = JsonConvert.DeserializeObject<ItemData>(jsonResult);
            foreach (var item in itemData.Items)
            {
                string path = $"Textures/Items/{item.Category}/{item.Name}";
                Sprite sprite = Resources.Load<Sprite>(path);
                if (sprite != null)
                {
                    item.Icon = sprite;
                }
                else
                {
                    Debug.LogWarning($"Cannot load sprite at path: Resources/{path}");
                }
            }
        }
        catch (TaskCanceledException)
        {
            Debug.LogError("Item fetch was canceled.");
        }
        
        return itemData;
    }
}

public static class RarityLoader
{
    public static Dictionary<int, string> RarityDictionary = new Dictionary<int, string>()
    {
        { 0,"Textures/Items/Rarity/ItemRarityCommon" },
        { 1,"Textures/Items/Rarity/ItemRarityUncommon" },
        { 2,"Textures/Items/Rarity/ItemRarityRare" },
        { 3,"Textures/Items/Rarity/ItemRarityEpic"},
        { 4,"Textures/Items/Rarity/ItemRarityLegendary"}
    };

    public static Sprite GetTextureToLoad(int rarity)
    {
        return Resources.Load<Sprite>(RarityDictionary[rarity]);
    }

}