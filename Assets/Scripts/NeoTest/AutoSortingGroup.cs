using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AutoSortingGroup : MonoBehaviour
{
    private SortingGroup sg;

    private void Awake()
    {
        sg = GetComponent<SortingGroup>();
        sg.sortingOrder = Mathf.RoundToInt(transform.position.y);
    }

}
