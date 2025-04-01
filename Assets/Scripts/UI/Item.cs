using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject        // 用于储存物品信息
{
    public string itemName;         // 物品名称
    public Sprite itemImage;        // 物体图片
    public int itemID;              // 物品ID
    public int itemHold;            // 物品数量
    [TextArea]
    public string itemInfo;         // 物品描述信息
}
