using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronParent : MonoBehaviour
{
    [SerializeField] private Patron patron;


    private bool block = false;

    public bool Block { get => block; }

    private IEnumerator Coldown()
    {
        block = true;
        yield return new WaitForSeconds(1f);
        block = false;
    }
    public Patron GetPatron(Vector3 spawnPoint)
    {
        if (block) return null;
        patron.transform.position = spawnPoint;
        patron.gameObject.SetActive(true);
        return patron;
    }
    public Patron GetPatron(Vector3 spawnPoint, Quaternion spawnRotation)
    {
        if (block) return null;
        patron.transform.rotation = spawnRotation;
        patron.transform.position = spawnPoint;
        patron.gameObject.SetActive(true);
        return patron;
    }
    public Patron GetPatron()
    {
        if (block) return null;
        return patron;
    }
    private void Awake()
    {
        patron?.OnKill.AddListener(() =>
        {
            StartCoroutine(Coldown());
            patron.AnimateExplose(() =>
            {
                patron.gameObject.SetActive(false);
            });
        });
    }

}
