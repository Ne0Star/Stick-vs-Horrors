using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;
    public float MaxDistance, MoveSpeed;

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > MaxDistance)
        {
            transform.position = Vector2.Lerp(transform.position, target.position, MoveSpeed);
        }
        else return;
    }

}
