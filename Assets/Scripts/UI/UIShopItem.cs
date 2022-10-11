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
    [SerializeField] private Text costValue, nameKey, discriptionKey, countValue;
    [SerializeField] private ShopItem shopItem;

    public UnityEvent<UIShopItem> OnClick { get => onClick; }
    public ShopItem ShopItem { get => shopItem; }


    private void Awake()
    {
        if (shopItem)
        {
            SetItem(shopItem);
        }
    }

    private void OnEnable()
    {
        if (shopItem != null)
            SetItem(shopItem);
    }

    private void SetText(Text text, string key)
    {
        LocalizerData data = GameManager.Instance.GetValueByKey(key);
        text.text = data.resultText;
        text.font = data.resultFont;
    }

    public void SetItem(ShopItem item, int count)
    {
        shopItem = item;
        if (image)
            image.sprite = item.image;
        if (countValue)
            countValue.text = count + "";
        if (costValue)
            costValue.text = item.cost + "";
        if (nameKey)
            SetText(nameKey, nameKey.text);
        if (discriptionKey)
            SetText(discriptionKey, discriptionKey.text);
        if (actionButton)
            actionButton.onClick.AddListener(() => { onClick?.Invoke(this); });
    }

    public void SetItem(ShopItem item)
    {
        shopItem = item;
        if (image)
            image.sprite = item.image;
        if (countValue)
            countValue.text = item.count + "";
        if (costValue)
            costValue.text = item.cost + "";
        if (nameKey)
            SetText(nameKey, nameKey.text);
        if (discriptionKey)
            SetText(discriptionKey, discriptionKey.text);
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
