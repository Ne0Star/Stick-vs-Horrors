using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviour
{
    public Dictionary<int, LimbStrength> limbs;
    [SerializeField] private int registerCount;

    private void Awake()
    {
        registerCount = 0;
        limbs = new Dictionary<int, LimbStrength>();
    }

    public void RegisterLimb(LimbStrength limb)
    {
        if(!limb || !limb.transform) return;
        limbs[limb.transform.GetInstanceID()] = limb;
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
            Debug.LogError("По заданному ключу не найдена конченость: " + id);
            return null;
        }
#endif
        return limbs[id];
    }

}
