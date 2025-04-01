using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;         // ��Ƶ�¼�
    public AudioClip audioClip;         // ��ѡ����Ƭ��
    public bool playOnEnable;           // �ڹ������õ�ʱ���ж������Ƿ񲥷�

    private void OnEnable()
    {
        // �ڹ������ú󲥷�����
        if (playOnEnable)
            PlayAudioClip();
    }
    public void PlayAudioClip()
    {
        // ��������Ƭ��
        playAudioEvent.OnEventRaised(audioClip);
    }
}
