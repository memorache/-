using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject        // ���ڴ�����Ʒ��Ϣ
{
    public string itemName;         // ��Ʒ����
    public Sprite itemImage;        // ����ͼƬ
    public int itemID;              // ��ƷID
    public int itemHold;            // ��Ʒ����
    [TextArea]
    public string itemInfo;         // ��Ʒ������Ϣ
}
