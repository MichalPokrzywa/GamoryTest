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
    public static Dictionary<int, RarityTexture> RarityItemDictionary = new Dictionary<int, RarityTexture>()
    {
        { 0,new RarityTexture(0,"Textures/Items/Rarity/ItemRarityCommon")  },
        { 1,new RarityTexture(1,"Textures/Items/Rarity/ItemRarityUncommon") },
        { 2,new RarityTexture(2, "Textures/Items/Rarity/ItemRarityRare") },
        { 3,new RarityTexture(3, "Textures/Items/Rarity/ItemRarityEpic")},
        { 4,new RarityTexture(4, "Textures/Items/Rarity/ItemRarityLegendary")}
    };

    public static Dictionary<int, RarityTexture> RarityItemBackgroundDictionary = new Dictionary<int, RarityTexture>()
    {
        { 0,new RarityTexture(0,"Textures/Items/Rarity/CardInDeckListCommonUI")  },
        { 1,new RarityTexture(1,"Textures/Items/Rarity/CardInDeckListUncommonUI") },
        { 2,new RarityTexture(2, "Textures/Items/Rarity/CardInDeckListEpicUI") },
        { 3,new RarityTexture(3, "Textures/Items/Rarity/CardInDeckListRareUI")},
        { 4,new RarityTexture(4, "Textures/Items/Rarity/CardInDeckListRareUI")}
    };
    public static Sprite GetTextureIconToLoad(int rarity)
    {
        return RarityItemDictionary[rarity].Sprite;
    }
    public static Sprite GetTextureBackgroundToLoad(int rarity)
    {
        return RarityItemBackgroundDictionary[rarity].Sprite;
    }

}

[System.Serializable]
public class RarityTexture
{
    public int RarityId;
    public Sprite Sprite;
    private string FileName;

    public RarityTexture(int rarityId, string fileName)
    {
        RarityId = rarityId;
        FileName = fileName;
        Sprite = Resources.Load<Sprite>(FileName);
    }
}