using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour, IInteractable     // 玩家拾取道具
{
    //public enum PickupType      // 设置可拾取道具的类型
    //{
    //    Egg,                // 任务道具，交付物品使用
    //    Weapon,             // 武器，用于获得不同技能
    //    HealingPotion       // 血瓶，治疗消耗用
    //}
    //// 设置道具类型
    //[SerializeField] private PickupType pickupType;
    //// 道具的值，例如拾取后对应道具数量+1
    //[SerializeField] private int value;
    // 创建数据库信息，用于让物体的数量产生变化
    public Item thisItem;
    // 获取玩家背包
    public Inventory playerInventory;
    // 使用IInteractable接口中的TriggerAction方法执行拾取到道具后的方法
    public void TriggerAction()
    {
        // 执行判断道具的方法
        CollectPickup();
    }
    // 判断道具的类型，根据不同类型执行不同逻辑
    private void CollectPickup()
    {
        // 如果背包内没有这个道具，那就在背包里获得这个道具，否则就相应道具数量+1
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
        }
        else
        {
            thisItem.itemHold++;
        }
        // 销毁道具
        Destroy(gameObject);
        // 刷新背包内道具
        UIManager.RefreshItem();
        //switch (pickupType)
        //{
        //    case PickupType.Egg:
        //        // 处理Egg逻辑
        //        HandleEggPickup();
        //        break;
        //    case PickupType.Weapon:
        //        // 处理Weapon逻辑
        //        HandleWeapinPickup();
        //        break;
        //    case PickupType.HealingPotion:
        //        // 处理HealingPotion逻辑
        //        HandleHealingPotionPickup();
        //        break;
        //}
    }
    // 拾取Egg道具后的逻辑
    //private void HandleEggPickup()
    //{
    //    // 如果背包内没有这个道具，那就在背包里获得这个道具，否则就相应道具数量+1
    //    
    //}
    //// 拾取Weapon道具后的逻辑
    //private void HandleWeapinPickup()
    //{
    //    // 在背包内显示

    //}
    //private void HandleHealingPotionPickup()
    //{
    //    // 背包内血瓶数量+1

    //}
}
