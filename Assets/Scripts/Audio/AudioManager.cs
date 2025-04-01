using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;        // 引用音频

public class AudioManager : MonoBehaviour
{
    [Header("组件")]
    public AudioSource BGMSource;       // 背景音乐
    public AudioSource FXSource;        // 音效
    [Header("事件监听")]
    public PlayAudioEventSO BGMEvent;   // 音乐监听
    public PlayAudioEventSO FXEvent;    // 音效监听

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
        // 指定当前音频并播放
        FXSource.clip = clip;
        FXSource.Play();
    }
    private void OnBGMEvent(AudioClip clip)
    {
        // 指定当前背景音乐并播放
        BGMSource.clip = clip;
        BGMSource.Play();
    }
}
