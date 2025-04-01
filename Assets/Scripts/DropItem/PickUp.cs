using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour, IInteractable     // ���ʰȡ����
{
    //public enum PickupType      // ���ÿ�ʰȡ���ߵ�����
    //{
    //    Egg,                // ������ߣ�������Ʒʹ��
    //    Weapon,             // ���������ڻ�ò�ͬ����
    //    HealingPotion       // Ѫƿ������������
    //}
    //// ���õ�������
    //[SerializeField] private PickupType pickupType;
    //// ���ߵ�ֵ������ʰȡ���Ӧ��������+1
    //[SerializeField] private int value;
    // �������ݿ���Ϣ����������������������仯
    public Item thisItem;
    // ��ȡ��ұ���
    public Inventory playerInventory;
    // ʹ��IInteractable�ӿ��е�TriggerAction����ִ��ʰȡ�����ߺ�ķ���
    public void TriggerAction()
    {
        // ִ���жϵ��ߵķ���
        CollectPickup();
    }
    // �жϵ��ߵ����ͣ����ݲ�ͬ����ִ�в�ͬ�߼�
    private void CollectPickup()
    {
        // ���������û��������ߣ��Ǿ��ڱ�������������ߣ��������Ӧ��������+1
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
        }
        else
        {
            thisItem.itemHold++;
        }
        // ���ٵ���
        Destroy(gameObject);
        // ˢ�±����ڵ���
        UIManager.RefreshItem();
        //switch (pickupType)
        //{
        //    case PickupType.Egg:
        //        // ����Egg�߼�
        //        HandleEggPickup();
        //        break;
        //    case PickupType.Weapon:
        //        // ����Weapon�߼�
        //        HandleWeapinPickup();
        //        break;
        //    case PickupType.HealingPotion:
        //        // ����HealingPotion�߼�
        //        HandleHealingPotionPickup();
        //        break;
        //}
    }
    // ʰȡEgg���ߺ���߼�
    //private void HandleEggPickup()
    //{
    //    // ���������û��������ߣ��Ǿ��ڱ�������������ߣ��������Ӧ��������+1
    //    
    //}
    //// ʰȡWeapon���ߺ���߼�
    //private void HandleWeapinPickup()
    //{
    //    // �ڱ�������ʾ

    //}
    //private void HandleHealingPotionPickup()
    //{
    //    // ������Ѫƿ����+1

    //}
}
