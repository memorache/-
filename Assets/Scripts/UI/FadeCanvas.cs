using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;      // ����DoTween�����ռ�

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;     // ��ȡ���뵭����Image���
    [Header("�¼�����")]
    public FadeEventSO fadeEvent;     // ����FadeEventSO�¼�
    private void OnEnable()
    {
        // �����¼�����
        fadeEvent.OnRaisedEvent += OnFadeEvent;
    }
    private void OnDisable()
    {
        // ע���¼�����
        fadeEvent.OnRaisedEvent -= OnFadeEvent;
    }
    public void OnFadeEvent(Color target, float duration,bool fadeIn)       // ���뵭������
    {
        // ʹ��DoTween�����DOBlendableColor������ʵ����ɫ����
        fadeImage.DOBlendableColor(target, duration);
    }
}
