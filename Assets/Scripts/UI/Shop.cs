using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using static YG.SavesYG;

public class Shop : MonoBehaviour
{

    public ShopManager shopManager;

    public ItemCategory category;
    [SerializeField] private PreviewItem preview;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private List<UIShopItem> uiItems;
    [SerializeField] private UIShopItem itemPrefab;

    private void Start()
    {
        if (!itemPrefab) return;
        shopManager = FindObjectOfType<ShopManager>();
        List<ShopItem> items = new List<ShopItem>();
        foreach (ShopItem item in shopManager.initialShopItems)
        {
            if (item.category == category)
            {
                CreateItem(item);
            }
        }
        GameManager.Instance.onByu.AddListener(SetShop);
        SetShop();
        //uiItems = itemsParent.GetComponentsInChildren<UIShopItem>();
    }
    private void CreateItem(ShopItem item)
    {
        UIShopItem i = Instantiate<UIShopItem>(itemPrefab, itemsParent);
        i.OnClick?.AddListener((i) =>
        {
            SetPreviewItem(item);
        });
        i.SetItem(item);
        i.gameObject.SetActive(false);
        uiItems.Add(i);
    }

    public void SetShop()
    {
        Debug.Log("сет схоп");
        foreach (UIShopItem item in uiItems)
        {
            item.gameObject.SetActive(false);
            item.SetItem(item.ShopItem);
            if (YandexGame.savesData.items != null)
            {
                bool contains = false;
                foreach (InventoryData id in YandexGame.savesData.items)
                {
                    if (id.id == item.ShopItem.id)
                    {
                        contains = true;
                        break;
                    }
                }
                // Если предмет уже куплен
                if(contains)
                {
                    if(item.ShopItem.countType == ItemType.Множественный)
                    {
                        item.gameObject.SetActive(true);
                    }


                } else
                {
                    item.gameObject.SetActive(true);
                }
            }
            else
            {
                item.gameObject.SetActive(true);
            }
            //item.gameObject.SetActive(true);
        }
    }

    private void RemoveShop()
    {
        foreach (UIShopItem item in uiItems)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        RemovePreviewItem();
        SetShop();
    }

    private void OnDisable()
    {
        RemovePreviewItem();
        RemoveShop();
    }

    public void SetPreviewItem(ShopItem item)
    {
        preview.SetPreviewItem(item);
    }

    private void RemovePreviewItem()
    {
        preview.RemovePreviewItem();
    }
}
