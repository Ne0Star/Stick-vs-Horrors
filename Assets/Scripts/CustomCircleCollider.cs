using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCircleCollider : CustomCollider
{
    [SerializeField] protected float radius;
    [SerializeField] protected Vector3 offset;



    private void Update()
    {
        if (!isTrigger) return;



    }

    public override bool CheckPoint(Vector3 worldPos)
    {
        if (Vector2.Distance(transform.position + offset, worldPos) <= radius)
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }

}
