using UnityEngine;
using UnityEngine.Events;       // ����Events

[CreateAssetMenu(menuName ="Event/PlayerAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;        // ʹ��AudioClip��Ƶ��������������Ч
    public void RaiseEvent(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);       
    }
}
