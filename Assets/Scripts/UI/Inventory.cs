using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject       // ���ڱ����洢��Ʒ
{
    // ʹ���б�ķ�ʽȥ�洢���Ǳ��������е���Ʒ
    public List<Item> itemList = new List<Item>();
}
