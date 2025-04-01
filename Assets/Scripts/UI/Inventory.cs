using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject       // 用于背包存储物品
{
    // 使用列表的方式去存储我们背包中所有的物品
    public List<Item> itemList = new List<Item>();
}
