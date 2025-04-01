using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;       // 获取储存的装备信息
    public Sprite slotImage;     // 用于后续更改图片
    public Text slotNum;        // 显示对应物品数量

    public void ItemOnClicked()         // 用于处理物品点击事件，挂载在slot的OnClick上
    {
        UIManager.UpdateItemInfo(slotItem.itemInfo);
    }
}
