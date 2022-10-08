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
    [SerializeField] private float time;
    [SerializeField] private PatronParent patronPrefab;
    [SerializeField] private List<PatronParent> allPatrons;


    private void Awake()
    {
        for (int i = 0; i < batchCount; i++)
        {
            PatronParent patron = Instantiate<PatronParent>(patronPrefab);
            patron.transform.parent = transform;
            allPatrons.Add(patron);
            patron.GetPatron().gameObject.SetActive(false);
        }
        StartCoroutine(PatronLife());
    }

    private IEnumerator PatronLife()
    {

        foreach(PatronParent patron in allPatrons)
        {
            patron.Tick();
        }

        yield return new WaitForSeconds(time);
        StartCoroutine(PatronLife());
    }

    public Patron GetFreePatron(Vector3 worldSpawnPoint)
    {
        PatronParent result = null;
        foreach (PatronParent p in allPatrons)
        {
            if (p.GetPatron())
            {
                if (!p.GetPatron().gameObject.activeInHierarchy)
                {
                    result = p;
                    break;
                }
            }
        }
        result.gameObject.SetActive(true);
        return result.GetPatron(worldSpawnPoint);
    }
    public Patron GetFreePatron(Vector3 worldSpawnPoint, Quaternion spawnRotation)
    {
        PatronParent result = null;
        foreach (PatronParent p in allPatrons)
        {
            if (p.GetPatron())
            {
                if (!p.GetPatron().gameObject.activeInHierarchy)
                {
                    result = p;
                    break;
                }
            }
        }
        result.gameObject.SetActive(true);
        return result.GetPatron(worldSpawnPoint, spawnRotation);
    }
}
