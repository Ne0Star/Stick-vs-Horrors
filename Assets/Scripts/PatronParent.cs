using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronParent : MonoBehaviour
{
    [SerializeField] private Patron patron;

    public Patron GetPatron(Vector3 spawnPoint)
    {
        patron.transform.position = spawnPoint;
        patron.gameObject.SetActive(true);
        return patron;
    }

    private void Awake()
    {
        patron?.OnKill.AddListener(() =>
        {
            patron.transform.localPosition = Vector2.zero;
            gameObject.SetActive(false);
        });
    }

}
