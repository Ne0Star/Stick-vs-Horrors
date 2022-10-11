using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Storage : MonoBehaviour
{

    public int batchCount;
    public UIShopItem itemPrefab;
    public Transform itemsParent;
    [SerializeField] private PreviewItem preview;
    [SerializeField] private List<UIShopItem> items;

    private void Awake()
    {
        for (int i = 0; i < batchCount; i++)
        {
            UIShopItem item = Instantiate(itemPrefab, itemsParent);
            item.gameObject.SetActive(false);
            item.OnClick?.AddListener((i) =>
            {
                SetPreviewItem(item.ShopItem);
            });
            items.Add(item);
        }
    }
    public void SetPreviewItem(ShopItem item)
    {
        preview.SetPreviewItem(item);
    }

    private void RemovePreviewItem()
    {
        preview.RemovePreviewItem();
    }
    private void AddItem(ShopItem newItem, int count)
    {
        foreach (UIShopItem item in items)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
                item.SetItem(newItem, count);
                return;
            }
        }
        Debug.LogError("В хранилище недостаточно мест что бы отобразить все предметы");
    }

    private void OnEnable()
    {
        if (YandexGame.savesData.items != null)
        {
            foreach (InventoryData id in YandexGame.savesData.items)
            {
                //ShopItem item = null;
                //bool result = GameManager.Instance.ShopManager.allShopItems.TryGetValue(id.id, out item);
                if (GameManager.Instance.ShopManager.allShopItems != null)
                    AddItem(GameManager.Instance.ShopManager.allShopItems[id.id], id.count);
            }
        }
    }

    private void OnDisable()
    {
        foreach (UIShopItem item in items)
            item.gameObject.SetActive(false);
    }

}
