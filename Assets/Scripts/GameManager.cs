using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YG;
using static YG.SavesYG;

public struct LocalizerData
{
    public string resultText;
    public Font resultFont;
}

public class GameManager : OneSingleton<GameManager>
{
    public UnityEvent onByu;

    [SerializeField] private string currentLang = "ru";
    [SerializeField] private InfoYG infoYg;
    [SerializeField] private TextAsset scvLanguages;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UIPage loadPage;

    public ShopManager ShopManager { get => shopManager; }

    private void Awake()
    {
        GameManager.Instance = this;
        shopManager = FindObjectOfType<ShopManager>();
        uiManager = FindObjectOfType<UIManager>();
        scvLanguages = Resources.Load<TextAsset>("TranslateCSV");
        YandexGame.SwitchLanguage(currentLang);
    }

    private int GetLangIndex(string lang)
    {
        switch (lang)
        {
            case "ru": return 0;
            case "en": return 1;
            case "tr": return 2;
            case "az": return 3;
            case "be": return 4;
            case "he": return 5;
            case "hy": return 6;
            case "ka": return 7;
            case "et": return 8;
            case "fr": return 9;
            case "kk": return 10;
            case "ky": return 11;
            case "lt": return 12;
            case "lv": return 13;
            case "ro": return 14;
            case "tg": return 15;
            case "tk": return 16;
            case "uk": return 17;
            case "uz": return 18;
            case "es": return 19;
            case "pt": return 20;
            case "ar": return 21;
            case "id": return 22;
            case "ja": return 23;
            case "it": return 24;
            case "de": return 25;
            case "hi": return 26;
        }
        return -1;

    }

    public LocalizerData GetValueByKey(string key)
    {
        string[] values = CSVManager.ImportTransfersByKey("TranslateCSV", 27, key);

        if (values == null)
        {
            return new LocalizerData
            {
                resultFont = null,
                resultText = "null :)"
            };
        };
        int index = GetLangIndex(currentLang);
        string result = values[index];
        if (!infoYg || infoYg.fonts == null)
        {
            return new LocalizerData
            {
                resultFont = null,
                resultText = "null :)"
            };
        }
        Font[] fonts = infoYg.fonts.GetFontsByLanguageName(currentLang);
        Font resultFont = null;
        bool font = false;
        if (fonts != null)
            foreach (Font f in fonts)
            {
                if (f != null)
                {
                    resultFont = f;
                    font = true;
                    break;
                }
            }
        if (!font)
        {
            foreach (Font f in infoYg.fonts.defaultFont)
            {
                if (f != null)
                {
                    resultFont = f;
                    break;
                }
            }
        }

        //        if (values.Length < GetLangIndex(currentLang))
        //        {
        //result = values[GetLangIndex(currentLang)];
        //        }
        return new LocalizerData
        {
            resultFont = resultFont,
            resultText = result
        };
    }


    private void Start()
    {
        YandexGame.SwitchLangEvent += ((s) =>
        {
            currentLang = s;
        });


    }

    public void TestMethod()
    {
        YandexGame.savesData.Money += 1000;
        YandexGame.SaveProgress();
        onByu?.Invoke();
    }

    public void ByuItem(ShopItem shopItem)
    {
        if (shopItem != null)
        {

            if (YandexGame.savesData.Money - shopItem.cost <= 0)
            {
                Debug.Log("Недостаточно средств: " + YandexGame.savesData.Money + " / " + (YandexGame.savesData.Money - shopItem.cost));
                return;
            }

            // Получить список покупок
            InventoryData[] items = YandexGame.savesData.items;

            // Покупка существуюзего предмета
            bool isNew = true;
            if (items != null)
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].id == shopItem.id)
                    {
                        isNew = false;
                        if (shopItem.countType == ItemType.Множественный)
                        {
                            if (YandexGame.savesData.Money - shopItem.cost >= 0)
                            {
                                //Debug.Log("Было " + YandexGame.savesData.items[i].count);
                                YandexGame.savesData.Money -= shopItem.cost;
                                YandexGame.SaveProgress();
                                YandexGame.savesData.items[i].count += shopItem.count;
                                YandexGame.SaveProgress();
                                //Debug.Log("Стало " + YandexGame.savesData.items[i].count);
                            }
                            else
                            {
                                Debug.Log("Недостаточно средств");
                            }

                            break;
                        }
                        else if (shopItem.countType == ItemType.Одиночный)
                        {
                            Debug.Log("Это одиночный предмет и он уже приобетён");
                            break;
                        }
                    }
                }
            // Покупка нового предмета
            if (isNew)
            {
                Debug.Log("Покупка нового преджмета");
                List<InventoryData> newItems = new List<InventoryData>();

                if (YandexGame.savesData.items != null)
                    for (int i = 0; i < items.Length; i++)
                    {
                        newItems.Add(items[i]);


                    }

                newItems.Add(new InventoryData()
                {
                    count = shopItem.count,
                    id = shopItem.id
                });
                YandexGame.savesData.Money -= shopItem.cost;
                YandexGame.SaveProgress();
                YandexGame.savesData.items = newItems.ToArray();
                YandexGame.SaveProgress();

            }

            //YandexGame.SaveProgress();
            onByu?.Invoke();
        }
        else
        {
            Debug.Log("Неудалось купить: " + shopItem);
        }
    }

    public void SwitchScene(int index)
    {
        StartCoroutine(OpenSceneAsyncSingle(index));
    }
    public IEnumerator OpenSceneAsyncSingle(int index)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        Scene scene = gameObject.scene;
        if (loadPage) uiManager.OpenPage(loadPage);
        while (!time.isDone)
        {
            //if (loadText)
            //{
            //    float progress = Mathf.Clamp01(time.progress / 1.05f);
            //    loadText.text = progress + "";
            //}
            yield return new WaitForFixedUpdate();
        }
    }
}
