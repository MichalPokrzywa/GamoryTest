using System.IO;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Newtonsoft.Json;
using static UnityEditor.Progress;
using UnityEngine.Rendering;

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
