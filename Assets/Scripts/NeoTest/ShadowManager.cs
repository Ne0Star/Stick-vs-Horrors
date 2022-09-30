using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{

    public void AddShadow(CustomShadow shadow)
    {
        if (shadow == null) return;

        shadow.OnVisible += (v) =>
        {
            if (v)
            {
                activeShadow.Add(shadow);
            }
            else
            {
                activeShadow.Remove(shadow);
            }
        };

        allShadow.Add(shadow);
    }


    [SerializeField] private float updateTime;
    public List<CustomShadow> allShadow;
    public List<CustomShadow> activeShadow;

    private void Awake()
    {

    }
    private void Start()
    {
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        foreach (CustomShadow shadow in activeShadow)
        {
            if (!shadow.gameObject.activeInHierarchy) continue;
            shadow.Draw();

        }
        yield return new WaitForSeconds(updateTime);
        StartCoroutine(Life());
    }

}
