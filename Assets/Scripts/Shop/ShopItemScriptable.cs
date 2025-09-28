using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    standard, funky
}

[CreateAssetMenu(menuName = "Scriptables/ShopItem", fileName = "New Shop Item", order = 0)]
public class ShopItemScriptable : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType type;
    public Sprite icon;
    public float cost;
    public ResourceScriptable costType;
}
