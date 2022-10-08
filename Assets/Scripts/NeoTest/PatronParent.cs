using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronParent : MonoBehaviour
{
    [SerializeField] private Patron patron;
    [SerializeField] private float coldownTime = 5f, currentTime = 0f;

    private bool block = false;
    private bool coldown;
    public bool Block { get => block; }

    //private IEnumerator Coldown()
    //{
    //    block = true;
    //    yield return new WaitForSeconds(1f);
    //    block = false;
    //}
    private void StartColdown()
    {
        block = true;
        coldown = true;
        currentTime = 0f;
        Tick();
    }
    public void Tick()
    {
        if (!coldown) return;
        if (currentTime >= coldownTime)
        {
            block = false;
            coldown = false;
            currentTime = 0;
        }
        currentTime += 0.1f;
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
            StartColdown();
            patron.AnimateExplose(() =>
            {
                patron.gameObject.SetActive(false);
            });
        });
    }

}
