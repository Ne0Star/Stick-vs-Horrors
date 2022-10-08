using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private UnityEvent<UIShopItem> onClick;
    [SerializeField] private Image image;
    [SerializeField] private Text costValue, nameKey, discriptionKey;
    [SerializeField] private ShopItem shopItem;

    public UnityEvent<UIShopItem> OnClick { get => onClick; }
    public ShopItem ShopItem { get => shopItem; }


    private void Awake()
    {
        if(shopItem)
        {
            SetItem(shopItem);
        }
    }

    public void SetItem(ShopItem item)
    {
        if (image)
            image.sprite = item.image;
        if (costValue)
            costValue.text = item.cost + "";
        if (nameKey)
            nameKey.text = GameManager.Instance.GetValueByKey(nameKey.text);
        if (discriptionKey)
            discriptionKey.text = GameManager.Instance.GetValueByKey(discriptionKey.text);
        if (actionButton)
            actionButton.onClick.AddListener(() => { onClick?.Invoke(this); });

    }


    //private void OnDrawGizmos()
    //{
    //    if (shopItem)
    //    {
    //        SetItem(shopItem);
    //    }
    //    else
    //    {
    //        if (image)
    //            image.sprite = null;
    //        if (costValue)
    //            costValue.text = "";
    //        if (nameKey)
    //            nameKey.text = "";
    //        if (discriptionKey)
    //            discriptionKey.text = "";
    //        if (actionButton)
    //        {
    //            actionButton.onClick?.RemoveAllListeners();
    //            actionButton.onClick.AddListener(() => { onClick?.Invoke(this); });
    //        }

    //    }
    //}

}
