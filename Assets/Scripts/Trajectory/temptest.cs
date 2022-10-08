using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class temptest : MonoBehaviour
{
    public float damage;
    public float radius;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LevelManager.Instance.LimbManager.TakeSplashDamage(damage, radius, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
