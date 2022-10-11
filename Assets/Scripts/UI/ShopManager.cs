using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


/// <summary>
/// Отвечает за все покупные предметы
/// </summary>
public class ShopManager : MonoBehaviour
{
    /// <summary>
    /// Все предметы на продажу котоыре есть в игре
    /// </summary>
    public List<ShopItem> initialShopItems = new List<ShopItem>();
   public Dictionary<int, ShopItem> allShopItems;
    //public UnityEngine.Events.UnityEvent onStart;


    private void Awake()
    {
        PreInitial();
        //allShopItems = initialShopItems.ToDictionary(k => k.id);
    }



    //public int testKey;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log(allShopItems[testKey]);
    //    }
    //}

    private void PreInitial()
    {
        int i = 0;
        allShopItems = new Dictionary<int, ShopItem>();
        foreach (ShopItem item in initialShopItems)
        {
            allShopItems.Add(i, item);
            item.id = i;
            i++;
        }
    }


    [SerializeField] private bool setId;
    private void OnDrawGizmos()
    {
        if (setId)
        {
            PreInitial();
            setId = false;
        }
    }

}
