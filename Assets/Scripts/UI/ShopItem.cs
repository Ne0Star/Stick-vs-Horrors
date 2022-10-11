using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ByuItems/ByuItem", order = 1)]
public class ShopItem : ScriptableObject
{
    public Sprite image;
    [Header("Если не активно, 'nameKey' является названием оружия")]
    public bool useTranslatorName = false;

    public string nameKey, discriptionKey;
    public int id;
    public int count;
    public int cost;

    public ItemType countType;
    public ItemCategory category;
}
public enum ItemType
{
    Множественный,
    Одиночный
}

public enum ItemCategory
{
    Оружие,
    Броня,
    Помощь
}