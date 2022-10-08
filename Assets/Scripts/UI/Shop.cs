using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button byuButton;
    [SerializeField] private Text previewName, previewDescription, previewCost;
    [SerializeField] private Image previewImage;
    [SerializeField] private ShopItem previewItem;
    [SerializeField] private Transform itemView;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private UIShopItem[] uiItems;

    private void Awake()
    {
        uiItems = itemsParent.GetComponentsInChildren<UIShopItem>();
        byuButton.onClick?.AddListener(() =>
        {

            GameManager.Instance.ByuItem(previewItem);

        });
        foreach (UIShopItem item in uiItems)
        {
            item.OnClick?.AddListener((i) =>
            {
                SetPreviewItem(i.ShopItem);
            });
        }

    }

    private void OnEnable()
    {
        RemovePreviewItem();
    }

    private void OnDisable()
    {
        RemovePreviewItem();
    }

    public void SetPreviewItem(ShopItem item)
    {
        previewItem = item;
        previewCost.text = item.cost + "";
        previewImage.sprite = item.image;
        previewName.text = GameManager.Instance.GetValueByKey(item.nameKey);
        previewDescription.text = GameManager.Instance.GetValueByKey(item.discriptionKey);
        itemView.gameObject.SetActive(true);
    }

    private void RemovePreviewItem()
    {
        previewItem = null;
        previewCost.text = "";
        previewImage.sprite = null;
        previewName.text = "";
        previewDescription.text = "";
        itemView.gameObject.SetActive(false);
    }

    private bool setGizmozItem = false;

    private void OnDrawGizmos()
    {
        if (setGizmozItem)
        {
            if (previewItem)
            {
                SetPreviewItem(previewItem);
            }
            else
            {
                previewCost.text = "";
                previewImage.sprite = null;
                previewName.text = "";
                previewDescription.text = "";
            }
            setGizmozItem = false;
        }

    }

}
