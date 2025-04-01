using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;           // Switch�ֱ�
using UnityEngine.InputSystem.XInput;           // PS�ֱ�
using UnityEngine.InputSystem.DualShock;
using Unity.VisualScripting;        // xbox�ֱ�

public class Sign : MonoBehaviour
{
    private PlayerInputControl inputControl;        // ��ȡ����ϵͳ���
    private SpriteRenderer spriteRenderer;          // ���ں�����ȡָ�������е�spriteRenderer���
    private Animator anim;              // ��ȡ������Ķ���
    public Transform playerTrans;   // ���ڻ�ȡ��ҷ���
    public GameObject signSprite;       // ���ڿ��ذ�����ʾ
    private IInteractable targetItem;   // ʹ�ýӿڻ�õ�ǰ�����пɻ���������
    private bool canPress;              // �жϵ�ǰ�Ƿ���Ե㰴

    private void Awake()
    {   
        // ���ֻ�ȡ�����嶯���ķ���������ʹ�õڶ��֣���Ϊ������ʾһ��ʼ�ǹرյģ���һ���޷��ڹر�ʱ��ȡ
        // anim = GetComponentInChildren<Animator>();
        anim = signSprite.GetComponent<Animator>();
        // ��ȡ�Ӷ����spriteRenderer
        spriteRenderer = signSprite.GetComponent<SpriteRenderer>();

        // ʵ����inputControl
        inputControl = new PlayerInputControl();
        // ����inputControl
        inputControl.Enable();
    }
    private void OnEnable()
    {
        // ʹ��unity�Դ���onActionChange����ʵ�ֲ�ͬ�����豸������������
        InputSystem.onActionChange += OnActionChange;
        // ʵ�ֳ��������뽻���İ���
        inputControl.GamePlay.Confirm.started += OnConfirm;
        // 
    }
    private void OnDisable()
    {
        
    }
    private void Update()
    {
        // ���canPress�����˾͵���UI������Ϣ
        spriteRenderer.enabled = canPress;
        // ��ȡplayer��ط��򲢸�ֵ����������
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        // ��TMΪʲô����д���ٷ�API�ĵ�Ҳ�鲻��
        // һ���������豸���л��˲�����ִ�к���Ĵ���
        if(actionChange == InputActionChange.ActionStarted)
        {
            // ���objȷ����ǰ�����豸��ʲô
            // Debug.Log(((InputAction)obj).activeControl.device);
            // �����ֵ��ֵ��d���ں���Switch�л�
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;
                // �ֱ��������ʹ��Gamepadͳһ�����������ڸ��ֱ�����������ͬ�ᵼ�°�����һ��������ͷֿ�д
                // case Gamepad:
                case XInputController:
                    anim.Play("Xbox");
                    break;
            }
        }
    }
    private void OnConfirm(InputAction.CallbackContext context)         // ���ڰ���ȷ���¼�
    {
        if (canPress)
        {
            // ʹ�ö�Ӧ����ʵ�ֵĽӿ����еķ�����ʵ�ֻ���
            targetItem.TriggerAction();
            // ���Ŷ�Ӧ�����ϵ���Ƶ

        }
    }
    private void OnTriggerStay2D(Collider2D other)         // �ж�Tag��ɻ����������ײ
    {
        // �ж��ǲ���ײ��һ����
        if (other.CompareTag("Interactable"))
        {
            // ײס�˾��ð���������ʾ
            canPress = true;
            // ��ײ����������ʱ����ȡ���������ϵĽӿ�
            targetItem = other.GetComponent<IInteractable>();
        }
        else
        {
            // ײ�����Ͳ���ʾ
            canPress = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)          // �ж�Tag��ɻ���������뿪
    {
        if(other.CompareTag("Interactable"))
        {
            // �뿪�˾��ð�������ʾ
            canPress = false;
        }
    }
}
