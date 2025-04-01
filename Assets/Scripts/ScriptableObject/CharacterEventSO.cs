using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]      // 创建资产文件，让它可以在菜单中被创建
public class CharacterEventSO : ScriptableObject        // 改变继承文件为ScriptableObject
{
    // 声名OnEventRaised事件，创建UnityEvent<Character>的委托，当事件触发时，会携带一个Character类型的参数
    public UnityEvent<Character> OnEventRaised;
    public void RaiseEvent(Character character)         //订阅OnEventRaised事件
    {
        OnEventRaised?.Invoke(character);
    }
}