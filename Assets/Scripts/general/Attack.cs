using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("����������ֵ")]
    public int attack;      //����ֵ(����Damage����ΪDamage�����ڱ�ʾ�ܵ��Ĺ�����ֵ���ݷ���ֵ�ȵļ��ٺ��˺�)
    public float attackRange;       //��������
    public float attackRate;        //����Ƶ��

    private void OnTriggerStay2D(Collider2D other)      //ͨ�������ײ���Ƿ���ײ��ȷ���Ƿ񱻹���
    {
        //ͨ������Character���ܵ��˺��ķ���������ǰ�ܵ����˺�attack���ݵ�Character��
        //����?�����жϱ���ײ������û��Character��������û�оͲ�ִ��TakeDamege����ֹ����
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
