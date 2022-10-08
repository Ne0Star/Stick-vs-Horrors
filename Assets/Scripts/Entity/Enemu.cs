using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemu : Entity
{

    [SerializeField] protected LimbStrength[] limbs;

    private void Awake()
    {
        limbs = gameObject.GetComponentsInChildren<LimbStrength>(true);

    }
}
