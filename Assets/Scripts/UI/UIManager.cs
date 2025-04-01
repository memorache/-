using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;   // ����ģʽ��ֻ����һ������
    [Header("������ز���")]          // ���ڽ���Ʒ��ʾ������ڱ�����
    private PlayerInputControl inputControl;     // ���ڿ��Ʊ������صı���
    public GameObject bagUI;            // ѡ��򿪵ı���UI
    public Inventory bag;               // ѡ��Ҫ���ĵı���
    public GameObject slotGrid;         // ��������ͼƬ
    public Slot slotPrefab;             // ��������Ԥ����
    public TextMeshProUGUI itemInformation;         // �����·���Ʒ�����ĵ�
    [Header("Ѫ����ز���")]
    public PlayerStatBar playerStatBar;         // ����PlayerStatBar�ı������ڴ��ݰٷֱȲ���
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;        // ����healthEvent�¼����ڼ���Ѫ���仯ʱ�Ĳ���
    private void Awake()
    {
        // ��ʼ��inputControl�����ñ���UI�Ŀ����¼���Ŀǰδ֪ԭ���޷�ִ��
        inputControl = new PlayerInputControl();
        inputControl.UI.BagControl.started += OpenBag;
        // ����ģʽ������ȷ��ֻ����һ������
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable()         // ���ļ�������Ҫ��OnEnable��ע���¼�
    {
        // ���ڶ��ĵ���OnEventRaised�¼�������������ע�ἤ����
        healthEvent.OnEventRaised.AddListener(OnHealthEvent);
        // ����ʱˢ�±�����Ϣ
        RefreshItem();
        // ��ս����ı�
        instance.itemInformation.text = "";
    }
    private void OnDisable()        // ��OnDisable��ȡ��ע���¼�
    {
        // ���ڶ��ĵ���OnEventRaised�¼�������������ȡ��
        healthEvent.OnEventRaised.RemoveListener(OnHealthEvent);    
    }
    private void OnHealthEvent(Character character)         // �¼����������ķ���
    {
        // Ѫ���ٷֱ�
        var persentage = character.currentHealth / character.maxHealth;
        // ��Ѫ���ٷֱȴ��ݹ�ȥ
        playerStatBar.OnHealthChange(persentage);
    }
    private void OpenBag(InputAction.CallbackContext context)       // ���ڴ򿪱���
    {
        bagUI.SetActive(!bagUI.activeSelf);
    }
    public static void UpdateItemInfo(string itemDescription)
    {
        // �ڵ��װ�����ܺ���ʾ��װ���ı�����
        instance.itemInformation.text = itemDescription;
    }
    public static void CreateNewItem(Item item)         // �ڱ����ڴ�����������������ʾ������Ʒ
    {
        // ����Instantiate���������µı�������Ԥ����
        // ����λ��Ϊ��������ͼƬ����������ת�����ڱ�������ͼƬ��Grid LayOut Group��������Ի��Զ�����
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        // �������ӵ�λ�ã�������ݸ���ͼƬ��λ�ý��а���
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        // �����������Ӷ�Ӧ��ƷΪ���ݿ�ָ����Ʒ
        newItem.slotItem = item;
        // ����������������ƷͼƬ��ӦΪ���ݿ�ͼƬ
        newItem.slotImage = item.itemImage;
        // ��������������������ʾΪ���ݿ���Ʒ����
        newItem.slotNum.text = item.itemHold.ToString();
    }
    public static void RefreshItem()        // ˢ����Ʒ����
    {
        // ���α���ÿһ���������ӣ����û����Ʒ�Ͳ�ִ�����ѭ��
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            // ��ÿ����Ϸ������ʱ�ݻٱ��������б�������
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        // ���ݱ�����������Ʒ��˳���ڱ����ڴ����������ӣ�������ʾ��������
        for(int i = 0; i < instance.bag.itemList.Count; i++)
        {
            CreateNewItem(instance.bag.itemList[i]);
        }
    }
}