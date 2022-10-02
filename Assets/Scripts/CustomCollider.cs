using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomCollider : MonoBehaviour
{
    [SerializeField] protected bool isTrigger;

    /// <summary>
    /// ����� true ���� ����� ������
    /// </summary>
    /// <returns></returns>
    public abstract bool CheckPoint(Vector3 worldPos);
}
