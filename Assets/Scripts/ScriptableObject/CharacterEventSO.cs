using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]      // �����ʲ��ļ������������ڲ˵��б�����
public class CharacterEventSO : ScriptableObject        // �ı�̳��ļ�ΪScriptableObject
{
    // ����OnEventRaised�¼�������UnityEvent<Character>��ί�У����¼�����ʱ����Я��һ��Character���͵Ĳ���
    public UnityEvent<Character> OnEventRaised;
    public void RaiseEvent(Character character)         //����OnEventRaised�¼�
    {
        OnEventRaised?.Invoke(character);
    }
}