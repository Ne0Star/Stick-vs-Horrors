using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewItem : MonoBehaviour
{
    [SerializeField] private bool replaceFont = false;
    [SerializeField] private Button byuButton;
    [SerializeField] private ShopItem item;
    [SerializeField] private Image image;
    [SerializeField] private Text itemName, description, cost, count;

    public ShopItem Item { get => item; }
    public Button ByuButton { get => byuButton; }


    private void Start()
    {
        GameManager.Instance?.onByu?.AddListener(RemovePreviewItem);
    }

    public void SetPreviewItem(ShopItem item)
    {
        if (item == null)
        {

            Debug.LogWarning("PreviewItem: попытка установить нулевой предмет, была успешно устранена");

            return;
        }
        if (!gameObject)
        {
            Debug.LogWarning("PreviewItem: игровой объект не был назначен");

            return;
        }
        this.item = item;
        if (byuButton)
        {
            byuButton.onClick.RemoveAllListeners();
            byuButton.onClick?.AddListener(() =>
            {
                //if (item.countType == ItemType.Множественный)
                //{

                //}
                //else
                //{
                GameManager.Instance.ByuItem(item);
                //}
            });
        }
        if (count)
            count.text = item.count + "";
        if (cost)
            cost.text = item.cost + "";
        if (image)
            image.sprite = item.image;
        if (itemName)
        {
            LocalizerData data = GameManager.Instance.GetValueByKey(item.nameKey);

            if(item.useTranslatorName)
            {
                    itemName.font = data.resultFont;
            }

            itemName.text = item.useTranslatorName ? data.resultText : item.nameKey;
            if (replaceFont)
                itemName.font = data.resultFont;
        }

        if (description)
        {
            LocalizerData data = GameManager.Instance.GetValueByKey(item.discriptionKey);
            description.text = data.resultText;
            if (replaceFont)
                description.font = data.resultFont;
        }

        if (gameObject)
            gameObject.SetActive(true);
    }
    public void RemovePreviewItem()
    {
        item = null;
        if (cost)
            cost.text = "";
        if (image)
            image.sprite = null;
        if (count)
            count.text = "";
        if (itemName)
            itemName.text = "";
        if (description)
            description.text = "";
        gameObject.SetActive(false);
    }
}
