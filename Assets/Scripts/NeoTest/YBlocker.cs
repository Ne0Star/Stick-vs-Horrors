using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBlocker : MonoBehaviour
{
    [SerializeField] private float maxY;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -float.MaxValue, maxY), 0);
    }
}
