using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;         // 音频事件
    public AudioClip audioClip;         // 所选声音片段
    public bool playOnEnable;           // 在攻击启用的时候，判断音乐是否播放

    private void OnEnable()
    {
        // 在攻击启用后播放音乐
        if (playOnEnable)
            PlayAudioClip();
    }
    public void PlayAudioClip()
    {
        // 传递音乐片段
        playAudioEvent.OnEventRaised(audioClip);
    }
}
