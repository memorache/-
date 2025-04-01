using UnityEngine;
using UnityEngine.Events;       // 调用Events

[CreateAssetMenu(menuName ="Event/PlayerAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;        // 使用AudioClip音频储存容器播放音效
    public void RaiseEvent(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);       
    }
}
