using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Настройки которые косвенно вляют на тип потрона
/// </summary>
public struct PatronData
{


}

public class PatronPool : MonoBehaviour
{


    [SerializeField] private int batchCount;
    [SerializeField] private PatronParent patronPrefab;
    [SerializeField] private List<PatronParent> allPatrons;


    private void Awake()
    {
        for (int i = 0; i < batchCount; i++)
        {
            PatronParent patron = Instantiate<PatronParent>(patronPrefab);
            patron.transform.parent = transform;
            allPatrons.Add(patron);
            patron.gameObject.SetActive(false);
        }
    }

    public Patron GetFreePatron(Vector3 worldSpawnPoint)
    {
        PatronParent result = null;
        foreach (PatronParent p in allPatrons)
        {
            if (p)
                if (!p.gameObject.activeInHierarchy)
                {
                    result = p;
                    break;
                }
        }
        result.gameObject.SetActive(true);
        return result.GetPatron(worldSpawnPoint);
    }

}
