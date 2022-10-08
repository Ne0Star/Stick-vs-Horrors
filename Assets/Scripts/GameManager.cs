using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : OneSingleton<GameManager>
{
    [SerializeField] private string currentLang = "ru";
    [SerializeField] private TextAsset scvLanguages;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UIPage loadPage;

    private void Awake()
    {
        GameManager.Instance = this;
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

    public string GetValueByKey(string key)
    {
        string[] values = CSVManager.ImportTransfersByKey("TranslateCSV", 26, key);
        if (values == null) return "null :)";
        return values[GetLangIndex(currentLang)];
    }


    private void Start()
    {
        YandexGame.SwitchLangEvent += ((s) =>
        {
            currentLang = s;
        });


    }

    public void ByuItem(ShopItem item)
    {
        if (item != null)
        {
            Debug.Log("Попытка купить: " + item);
        }
        else
        {
            Debug.Log("Неудалось купить: " + item);
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
