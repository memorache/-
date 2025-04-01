using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;           // Switch手柄
using UnityEngine.InputSystem.XInput;           // PS手柄
using UnityEngine.InputSystem.DualShock;
using Unity.VisualScripting;        // xbox手柄

public class Sign : MonoBehaviour
{
    private PlayerInputControl inputControl;        // 获取输入系统组件
    private SpriteRenderer spriteRenderer;          // 用于后续获取指定对象中的spriteRenderer组件
    private Animator anim;              // 获取子物体的动画
    public Transform playerTrans;   // 用于获取玩家方向
    public GameObject signSprite;       // 用于开关按键提示
    private IInteractable targetItem;   // 使用接口获得当前场景中可互动的物体
    private bool canPress;              // 判断当前是否可以点按

    private void Awake()
    {   
        // 两种获取子物体动画的方法，这里使用第二种，因为按键提示一开始是关闭的，第一种无法在关闭时获取
        // anim = GetComponentInChildren<Animator>();
        anim = signSprite.GetComponent<Animator>();
        // 获取子对象的spriteRenderer
        spriteRenderer = signSprite.GetComponent<SpriteRenderer>();

        // 实例化inputControl
        inputControl = new PlayerInputControl();
        // 启用inputControl
        inputControl.Enable();
    }
    private void OnEnable()
    {
        // 使用unity自带的onActionChange方法实现不同输入设备按键动画播放
        InputSystem.onActionChange += OnActionChange;
        // 实现场景互动与交互的按键
        inputControl.GamePlay.Confirm.started += OnConfirm;
        // 
    }
    private void OnDisable()
    {
        
    }
    private void Update()
    {
        // 如果canPress正常了就调出UI按键信息
        spriteRenderer.enabled = canPress;
        // 获取player相关方向并赋值给按键方向
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        // 这TM为什么这样写，官方API文档也查不到
        // 一但启动了设备，切换了操作，执行后面的代码
        if(actionChange == InputActionChange.ActionStarted)
        {
            // 输出obj确定当前输入设备是什么
            // Debug.Log(((InputAction)obj).activeControl.device);
            // 将输出值赋值给d用于后续Switch切换
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;
                // 手柄这里可以使用Gamepad统一操作，但由于各手柄操作按键不同会导致按键不一样，这里就分开写
                // case Gamepad:
                case XInputController:
                    anim.Play("Xbox");
                    break;
            }
        }
    }
    private void OnConfirm(InputAction.CallbackContext context)         // 用于按键确认事件
    {
        if (canPress)
        {
            // 使用对应物体实现的接口类中的方法来实现互动
            targetItem.TriggerAction();
            // 播放对应物体上的音频

        }
    }
    private void OnTriggerStay2D(Collider2D other)         // 判断Tag与可互动物体的碰撞
    {
        // 判断是不是撞在一块了
        if (other.CompareTag("Interactable"))
        {
            // 撞住了就让按键可以显示
            canPress = true;
            // 在撞到其它物体时，获取该物体身上的接口
            targetItem = other.GetComponent<IInteractable>();
        }
        else
        {
            // 撞不到就不显示
            canPress = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)          // 判断Tag与可互动物体的离开
    {
        if(other.CompareTag("Interactable"))
        {
            // 离开了就让按键不显示
            canPress = false;
        }
    }
}
