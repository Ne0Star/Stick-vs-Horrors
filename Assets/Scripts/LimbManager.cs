using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviour
{
    public Dictionary<int, LimbStrength> limbs;
    [SerializeField] private List<LimbStrength> _limbs;
    [SerializeField] private int registerCount;
    [SerializeField] private float time;

    private void Awake()
    {
        registerCount = 0;
        limbs = new Dictionary<int, LimbStrength>();
        StartCoroutine(LimbsLife());
    }

    private IEnumerator LimbsLife()
    {

        foreach (LimbStrength limb in _limbs)
        {
            if (limb)
            {
                limb.Tick();
            }
        }
        yield return new WaitForSeconds(time);
        StartCoroutine(LimbsLife());
    }

    public void TakeSplashDamage(float damage, float radius, Vector3 worldPosition)
    {
        foreach (LimbStrength limb in _limbs)
        {
            if (limb && limb.gameObject)
            {
                float distance = Vector2.Distance(worldPosition, limb.transform.position);
                if (distance < radius)
                {
                    float percent = Mathf.InverseLerp(radius, 0, distance);
                    limb.TakeDamage(percent * damage, percent * damage / 2, worldPosition);
                }
            }
        }
    }

    public void RegisterLimb(LimbStrength limb)
    {
        if (!limb || !limb.transform) return;
        limbs[limb.transform.GetInstanceID()] = limb;
        _limbs.Add(limb);
        registerCount++;
    }

    public LimbStrength GetLimbById(int id)
    {
#if UNITY_EDITOR
        LimbStrength limb = null;
        if (limbs.TryGetValue(id, out limb))
        {
            return limb;
        }
        else
        {
            Debug.LogWarning("По заданному ключу не найдена конченость: " + id);
            return null;
        }
#endif
        return limbs[id];
    }

}
