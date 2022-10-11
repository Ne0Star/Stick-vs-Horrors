using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localizer : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string key;

    [SerializeField] private bool useOnEnable = false;
    [SerializeField] private bool useEvent = false;

    private void Awake()
    {
        if(!text) text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (useOnEnable)
        {
            Set();
        }
    }

    private void Set()
    {
        if (gameObject.activeInHierarchy)
        {
            LocalizerData data = GameManager.Instance.GetValueByKey(key);
            Debug.Log(data.resultFont);
            text.text = data.resultText;
            text.font = data.resultFont;
        }
    }

    private void OnDisable()
    {
        if (useEvent)
            YandexGame.SwitchLangEvent -= ((s) =>
            {
                Set();
            });
    }

    private void Start()
    {
        if (useEvent)
            YandexGame.SwitchLangEvent += ((s) =>
            {
                Set();
            });
    }
}
