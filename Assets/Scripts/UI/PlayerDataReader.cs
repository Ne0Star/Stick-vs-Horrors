using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerDataReader : MonoBehaviour
{
    [SerializeField] ImageLoadYG imageLoad;
    public Text money, score;
    public Text _name;

    private void Awake()
    {
        YandexGame.GetDataEvent += ReadAndWriteData;
    }

    public void ReadAndWriteData()
    {
        if(YandexGame.SDKEnabled)
        {
        if (_name != null)
            _name.text = YandexGame.playerName;
        if (money)
            money.text = YandexGame.savesData.Money + "";
        if (score)
            score.text = YandexGame.savesData.Score + "";
        if (imageLoad && YandexGame.auth)
            imageLoad.Load(YandexGame.playerPhoto);
        } else
        {
            Debug.LogError("SDK не активироваг");
        }

    }
}
