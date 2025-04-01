using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;        // ������Ƶ

public class AudioManager : MonoBehaviour
{
    [Header("���")]
    public AudioSource BGMSource;       // ��������
    public AudioSource FXSource;        // ��Ч
    [Header("�¼�����")]
    public PlayAudioEventSO BGMEvent;   // ���ּ���
    public PlayAudioEventSO FXEvent;    // ��Ч����

    private void OnEnable()
    {
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }
    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }
    private void OnFXEvent(AudioClip clip)
    {
        // ָ����ǰ��Ƶ������
        FXSource.clip = clip;
        FXSource.Play();
    }
    private void OnBGMEvent(AudioClip clip)
    {
        // ָ����ǰ�������ֲ�����
        BGMSource.clip = clip;
        BGMSource.Play();
    }
}
