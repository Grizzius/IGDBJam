using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new item", menuName = "Data/Item")]
public class Items : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite sprite;
    public Vector2Int size;
}
