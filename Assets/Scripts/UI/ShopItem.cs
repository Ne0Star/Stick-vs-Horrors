using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ByuItems/ByuItem", order = 1)]
public class ShopItem : ScriptableObject
{
    public Sprite image;
    public string nameKey, discriptionKey;
    public int id;
    public int cost;

}
