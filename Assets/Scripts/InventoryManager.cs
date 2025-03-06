using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private ItemLoader itemLoader = new ItemLoader();
    [SerializeField] private InventoryBag inventoryBagManager;
    private async void Start()
    {
        LoadingSpinner.OnLoadingStart.Invoke();
        StartCoroutine(inventoryBagManager.CreateItems(await itemLoader.FetchItems()));
        LoadingSpinner.OnLoadingEnd.Invoke();
    }
}
