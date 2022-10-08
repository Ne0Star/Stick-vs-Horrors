using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Text))]
public class Localizer : MonoBehaviour
{
    private Text text;
    [SerializeField] private string key;


    private void Start()
    {
        text = gameObject.GetComponent<Text>();
        YandexGame.SwitchLangEvent += ((s) =>
        {
            text.text = GameManager.Instance.GetValueByKey(key);
        });
    }
}
