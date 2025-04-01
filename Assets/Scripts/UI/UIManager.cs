using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;   // 单例模式，只存在一个背包
    [Header("背包相关参数")]          // 用于将物品显示盒添加在背包中
    private PlayerInputControl inputControl;     // 用于控制背包开关的变量
    public GameObject bagUI;            // 选择打开的背包UI
    public Inventory bag;               // 选定要更改的背包
    public GameObject slotGrid;         // 背包格子图片
    public Slot slotPrefab;             // 背包格子预制体
    public TextMeshProUGUI itemInformation;         // 背包下方物品介绍文档
    [Header("血量相关参数")]
    public PlayerStatBar playerStatBar;         // 创建PlayerStatBar的变量用于传递百分比参数
    [Header("事件监听")]
    public CharacterEventSO healthEvent;        // 创建healthEvent事件用于监听血量变化时的参数
    private void Awake()
    {
        // 初始化inputControl并设置背包UI的开关事件，目前未知原因无法执行
        inputControl = new PlayerInputControl();
        inputControl.UI.BagControl.started += OpenBag;
        // 单例模式，用于确保只创建一个背包
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable()         // 订阅监听后，需要在OnEnable中注册事件
    {
        // 由于订阅的是OnEventRaised事件，所以在这里注册激活它
        healthEvent.OnEventRaised.AddListener(OnHealthEvent);
        // 启用时刷新背包信息
        RefreshItem();
        // 清空介绍文本
        instance.itemInformation.text = "";
    }
    private void OnDisable()        // 在OnDisable中取消注册事件
    {
        // 由于订阅的是OnEventRaised事件，所以在这里取消
        healthEvent.OnEventRaised.RemoveListener(OnHealthEvent);    
    }
    private void OnHealthEvent(Character character)         // 事件调用启动的方法
    {
        // 血条百分比
        var persentage = character.currentHealth / character.maxHealth;
        // 将血量百分比传递过去
        playerStatBar.OnHealthChange(persentage);
    }
    private void OpenBag(InputAction.CallbackContext context)       // 用于打开背包
    {
        bagUI.SetActive(!bagUI.activeSelf);
    }
    public static void UpdateItemInfo(string itemDescription)
    {
        // 在点击装备介绍后显示该装备文本介绍
        instance.itemInformation.text = itemDescription;
    }
    public static void CreateNewItem(Item item)         // 在背包内创建背包格子用于显示背包物品
    {
        // 利用Instantiate函数创建新的背包格子预制体
        // 创建位置为背包格子图片，不设置旋转，由于背包格子图片有Grid LayOut Group组件，所以会自动排列
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        // 调整格子的位置，让其根据格子图片的位置进行摆正
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        // 调整背包格子对应物品为数据库指定物品
        newItem.slotItem = item;
        // 调整背包格子内物品图片对应为数据库图片
        newItem.slotImage = item.itemImage;
        // 调整背包格子内数量显示为数据库物品数量
        newItem.slotNum.text = item.itemHold.ToString();
    }
    public static void RefreshItem()        // 刷新物品数量
    {
        // 依次遍历每一个背包格子，如果没有物品就不执行这个循环
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            // 在每次游戏刚启动时摧毁背包内所有背包格子
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        // 根据背包内已有物品按顺序在背包内创建背包格子，用于显示背包物体
        for(int i = 0; i < instance.bag.itemList.Count; i++)
        {
            CreateNewItem(instance.bag.itemList[i]);
        }
    }
}