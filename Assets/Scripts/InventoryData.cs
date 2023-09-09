using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory data", menuName = "Data/Inventory")]
public class InventoryData : ScriptableObject
{
    public ItemRange[] items;
}

[System.Serializable]
public struct ItemRange
{
    public Items item;
    public int minimum;
    public int maximum;
}
