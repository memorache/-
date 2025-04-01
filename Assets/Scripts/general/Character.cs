using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;       //����Unity�¼�

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;      //���Ѫ��
    public float currentHealth;     //��ǰѪ��
    [Header("�����޵�֡������")]
    public float invulnerableDuration;      //�޵�ʱ��
    private float invulnerableCounter;      //�޵�ʱ�������
    public bool invulnerable = false;      //�Ƿ��޵�
    [Header("�¼�")]
    public UnityEvent<Character> OnHealthChange;    // �����¼���Ѫ���仯ʱ���ݲ���
    public UnityEvent<Transform> OnTakeDamage;      //����OnTakeDamage�¼���ʾ�ܵ��˺�ʱ�ᴥ���ķ��������¼���ִ�е�ʱ�򣬻�ִ��������ӵ����з���
    public UnityEvent Ondie;        //����Ondie�¼���ʾ������ᴥ���ķ���
    private void Start()
    {
        //��Ϸ��ʼ��Ѫ
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
                invulnerable = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collider)        // ���ڼ���ˮ�����ײ
    {
        if(collider.CompareTag("water"))
        {
            // Ѫ������
            currentHealth = 0;
            // ����Ѫ��
            OnHealthChange?.Invoke(this);
            // ִ�������¼�
            Ondie?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)       //׫д�ܵ��˺��ķ���������һ��Attack���attackerֵ����Attack���ݹ����ı���
    {
        //��������޵�ʱ�䣬�Ͳ�ִ�д���
        if (invulnerable)
            return;
        //�����ܵ����˺��Ƕ���
        //Debug.Log(attacker.attack);
        //�ж����˺���Ѫ��û
        if(currentHealth - attacker.attack > 0)
        {
            //���˼�������ֵ
            currentHealth -= attacker.attack;
            //�����޵�ʱ��
            TriggerInvulnerable();
            //�����¼�����ȷ�������з������������˶���
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            //��������
            currentHealth = 0;
            //ȷ�������������¼�����ȷ�������з�����������������
            Ondie?.Invoke();
        }
        // �����˺���������������ݸ����¼�һ�𴫵�
        OnHealthChange?.Invoke(this);
    }

    public void TriggerInvulnerable()       //�޵�ʱ�䴥������
    {
        invulnerable = true;
        invulnerableCounter = invulnerableDuration;
    }
}
