using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField] private HitBar hitBar;

    protected virtual void OnTakeDamage(float damage)
    {

    }

    /// <summary>
    /// Данная сущность получит урон
    /// </summary>
    public void TakeDamage(float damage)
    {
        OnTakeDamage(damage);
        hitBar.TakeDamage(damage, 0.1f, () =>
        {
            gameObject.SetActive(false);
        });
    }
}
