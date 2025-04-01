using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;       // ��ȡ�����װ����Ϣ
    public Sprite slotImage;     // ���ں�������ͼƬ
    public Text slotNum;        // ��ʾ��Ӧ��Ʒ����

    public void ItemOnClicked()         // ���ڴ�����Ʒ����¼���������slot��OnClick��
    {
        UIManager.UpdateItemInfo(slotItem.itemInfo);
    }
}
