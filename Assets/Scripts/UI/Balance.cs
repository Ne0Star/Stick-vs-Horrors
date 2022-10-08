using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Balance : UIPage
{

    [SerializeField] private Text moneyText;

    private void Start()
    {
            UpdateDisplayData();
    }

    private void UpdateDisplayData()
    {
        moneyText.text = YandexGame.savesData.money + " ";

    }

}
