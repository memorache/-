using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    // ��Ҫ���ݱ任��ɫ���任ʱ���Լ���boolֵ�жϽ��뻹�ǽ���
    public UnityAction<Color, float, bool> OnRaisedEvent;
    // �����н��뽥���������󷽷�������������ֱ𴴽���������
    /// <summary>
    /// �𽥱��,����
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        // �Դ�����Color.blackʵ����Ļ���
        RaiseEvent(Color.black, duration, true);
    }
    /// <summary>
    /// �𽥱�͸��������
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        // �Դ�����Color.clearʵ����Ļ͸��
        RaiseEvent(Color.clear, duration, false);
    }
    public void RaiseEvent(Color target,float duration,bool fadeIn)
    {
        // fadeInΪtrueʱ���룬Ϊfalseʱ����
        OnRaisedEvent?.Invoke(target, duration, fadeIn);
    }
}
