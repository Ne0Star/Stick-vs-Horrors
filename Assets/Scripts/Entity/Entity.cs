using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField] protected HitBar hitBar;

    /// <summary>
    /// Данная сущность получит урон
    /// </summary>
    public void TakeDamage(float damage)
    {
        hitBar.TakeDamage(damage, 0.1f, () =>
        {
            gameObject.SetActive(false);
        });
    }
}
