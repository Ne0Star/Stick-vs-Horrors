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

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ReadAndWriteData;
    }

    private void OnEnable()
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
            money.text = YandexGame.savesData.money + "";
        if (score)
            score.text = YandexGame.savesData.score + "";
        if (imageLoad && YandexGame.auth)
            imageLoad.Load(YandexGame.playerPhoto);
        }

    }
}
